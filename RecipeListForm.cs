using CookNet;
using CookNetLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CookNet
{
    public partial class RecipeListForm : Form
    {
        private string _category;
        private Form _parentForm;
        private RecipeManager _recipeManager = new RecipeManager();
        private FlowLayoutPanel pnlRecipes; // replaced ListBox with FlowLayoutPanel

        public RecipeListForm(string category, Form parentForm)
        {
            // This line calls the UI setup method defined in RecipeListForm.Designer.cs.
            InitializeComponent();

            _category = category;
            _parentForm = parentForm;

            this.Text = $"COOK.NET - {_category} Recipes";
            this.Size = new Size(700, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Set up the aesthetic and layout of the list screen
            SetupRecipeListUI();
            LoadRecipes();
        }

        // --- UI Setup Method ---
        private void SetupRecipeListUI()
        {
            this.BackgroundImage = Properties.Resources.ListBackground; // Unique background for list screen
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // 1. Panel for Banner (Top Section - Full Width Requirement)
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(60, 60, 60)
            };
            this.Controls.Add(pnlHeader);

            PictureBox pbBannerList = new PictureBox
            {
                Image = Properties.Resources.AppLogoBannerList, // Use the wide banner resource
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill // Ensure it fills the panel horizontally
            };
            pnlHeader.Controls.Add(pbBannerList);

            // 2. Back Button (Top Left)
            Button btnBack = new Button
            {
                Image = Properties.Resources.IconBack,
                ImageAlign = ContentAlignment.MiddleCenter,
                Size = new Size(40, 40),
                Location = new Point(10, 25), // Position within the form
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Text = "", // No text for icon button
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BackButton_Click;
            this.Controls.Add(btnBack);
            btnBack.BringToFront(); // Bring to the front of the background image

            // 3. Title Label (Centered)
            Label lblTitle = new Label
            {
                Text = $"{_category.ToUpper()} RECIPES",
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(100, 0, 0, 0), // Semi-transparent black overlay
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(0, 5, 0, 0)
            };
            this.Controls.Add(lblTitle);
            lblTitle.BringToFront();

            // 4. Recipe Panel (center area where recipe buttons will be added)
            pnlRecipes = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.Transparent,
                Width = 600,
                Height = 370,
                Location = new Point(50, 150), // Position below the banner and title
            };

            // center horizontally
            pnlRecipes.Location = new Point((this.ClientSize.Width - pnlRecipes.Width) / 2, pnlRecipes.Location.Y);

            // Allow panel to resize horizontally if form size changes (keeps centered-ish)
            pnlRecipes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.Controls.Add(pnlRecipes);
        }

        // --- Data Loading ---
        private void LoadRecipes()
        {
            List<string> recipeNames = RecipeManager.GetNameOfRecipe(_category) ?? new List<string>();

            // sensible fallbacks (preserve previous behavior)
            if (recipeNames.Count == 0)
            {
                string lower = _category?.ToLowerInvariant() ?? string.Empty;
                if (!string.Equals(lower, _category, StringComparison.Ordinal))
                {
                    recipeNames = RecipeManager.GetNameOfRecipe(lower) ?? new List<string>();
                }
            }
            if (recipeNames.Count == 0)
            {
                string plural = _category != null && _category.EndsWith("s", StringComparison.OrdinalIgnoreCase)
                    ? _category
                    : (_category + "s");
                recipeNames = RecipeManager.GetNameOfRecipe(plural) ?? new List<string>();
            }

            // Clear existing buttons
            pnlRecipes.SuspendLayout();
            pnlRecipes.Controls.Clear();

            // Create a button for each recipe (attempt to load icon)
            foreach (var recipeName in recipeNames)
            {
                Image icon = TryGetRecipeImage(recipeName) ?? Properties.Resources.IconDefaultRecipe;
                CreateRecipeButton(pnlRecipes, recipeName, icon);
            }

            pnlRecipes.ResumeLayout();

            // Re-center panel in case scrollbar changed width
            pnlRecipes.Location = new Point((this.ClientSize.Width - pnlRecipes.Width) / 2, pnlRecipes.Location.Y);
        }

        // --- Helper to create recipe buttons ---
        // Make `icon` optional by providing a default value; keep compatibility with existing callers.
        private void CreateRecipeButton(FlowLayoutPanel container, string recipeName, Image icon = null)
        {
            // Use container.ClientSize.Width AFTER layout to determine width; fallback to container.Width.
            int targetWidth = Math.Max(100, (container.ClientSize.Width > 0 ? container.ClientSize.Width : container.Width) - 20);

            // if icon is provided, ensure it's sized reasonably for the button
            Image? scaledIcon = null;
            int scaledIconWidth = 0;
            if (icon != null)
            {
                // keep aspect ratio, target height 40
                int targetIconHeight = 40;
                int targetIconWidth = (int)(icon.Width * (targetIconHeight / (double)icon.Height));
                scaledIcon = new Bitmap(icon, new Size(targetIconWidth, targetIconHeight));
                scaledIconWidth = scaledIcon.Width;
            }

            Button btn = new Button
            {
                Text = recipeName,
                Tag = recipeName,
                Image = scaledIcon,
                ImageAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 14, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                // overlay the text so it remains centered; add left padding to reserve space for the image
                TextImageRelation = TextImageRelation.Overlay,
                Size = new Size(targetWidth, 60), // fixed height, width matching container
                Margin = new Padding(10, 6, 10, 6),
                BackColor = Color.FromArgb(250, 250, 250),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Padding = new Padding(Math.Max(12, scaledIconWidth + 12), 0, 12, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += RecipeButton_Click;

            // Add then ensure width updates if the panel/client area changed (common when scrollbar appears)
            container.Controls.Add(btn);

            // force the button width to match the visible client area
            btn.Width = Math.Max(100, container.ClientSize.Width - 20);
        }

        // --- Tries multiple strategies to obtain a recipe image:
        //  1) look for a method on RecipeManager that accepts a string and returns Image/byte[]/string (path),
        //  2) try resource lookup by recipe name,
        //  3) fallback to null (caller will use placeholder).
        private Image TryGetRecipeImage(string recipeName)
        {
            // 1) Try methods on _recipeManager via reflection (safe at compile time).
            var mgrType = _recipeManager.GetType();
            string[] candidateNames = { "GetRecipePhoto", "GetPhoto", "GetRecipeImage", "GetImage", "GetPhotoOfRecipe" };
            foreach (var name in candidateNames)
            {
                var mi = mgrType.GetMethod(name, new[] { typeof(string) });
                if (mi == null) continue;
                try
                {
                    var result = mi.Invoke(_recipeManager, new object[] { recipeName });
                    if (result is Image img) return img;
                    if (result is byte[] bytes)
                    {
                        using (var ms = new MemoryStream(bytes))
                        {
                            return Image.FromStream(ms);
                        }
                    }
                    if (result is string path && File.Exists(path))
                    {
                        return Image.FromFile(path);
                    }
                }
                catch
                {
                    // ignore and continue to next strategy
                }
            }

            // 2) Try resources: assume resource name matches recipeName (sanitized)
            try
            {
                string resourceKey = new string(recipeName.Where(c => char.IsLetterOrDigit(c) || c == '_').ToArray());
                var resObj = Properties.Resources.ResourceManager.GetObject(resourceKey);
                if (resObj is Image resImg) return resImg;
            }
            catch
            {
                // ignore
            }

            // 3) No image found
            return null;
        }

        // --- Event Handlers ---
        private void RecipeButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button b && b.Tag is string selectedRecipeName)
            {
                RecipeDetailForm detailForm = new RecipeDetailForm(selectedRecipeName, this);
                detailForm.Show();
                this.Hide();
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _parentForm.Show();
            this.Close();
        }
    }
}

