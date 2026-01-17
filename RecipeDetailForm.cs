using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using CookNetLibrary;
using System.Web; // Required for URL encoding

namespace CookNet // <-- MUST MATCH YOUR PROJECT NAME
{
    // The 'partial' keyword is essential to link this file with RecipeDetailForm.Designer.cs
    public partial class RecipeDetailForm : Form
    {
        private Form _parentForm;
        private RecipeManager _recipeManager = new RecipeManager();
        private Recipe _currentRecipe;

        public RecipeDetailForm(string recipeName, Form parentForm)
        {
            // This line calls the UI setup method defined in RecipeDetailForm.Designer.cs.

            _currentRecipe = _recipeManager.GetRecipeInfo(recipeName).FirstOrDefault();
            _parentForm = parentForm;

            this.Text = $"COOK.NET - {_currentRecipe?.Name}";
            this.Size = new Size(800, 850);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Set up the aesthetic and layout of the detail screen
            SetupDetailUI();
        }

        // --- UI Setup Method ---
        private void SetupDetailUI()
        {
            this.BackgroundImage = Properties.Resources.KitchenBackground;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // 1. Back Button
            Button btnBack = new Button
            {
                Image = Properties.Resources.IconBack,
                ImageAlign = ContentAlignment.MiddleCenter,
                Size = new Size(45, 45),
                Location = new Point(10, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Text = "",
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BackButton_Click;
            this.Controls.Add(btnBack);

            // 2. Recipe Title
            Label lblTitle = new Label
            {
                Text = _currentRecipe.Name.ToUpper(),
                Font = new Font("Arial", 28, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(180, 0, 0, 0), // Semi-transparent overlay
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(0, 15, 0, 0)
            };
            this.Controls.Add(lblTitle);
            lblTitle.BringToFront();

            // 2. Back Button (Top Left)
            Button butnBack = new Button
            {
                Image = Properties.Resources.IconBack,
                ImageAlign = ContentAlignment.MiddleCenter,
                Size = new Size(45, 45),
                Location = new Point(10, 25), // Position within the form
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Text = "", // No text for icon button
            };
            butnBack.FlatAppearance.BorderSize = 0;
            butnBack.Click += BackButton_Click;
            this.Controls.Add(butnBack);
            butnBack.BringToFront(); // Bring to the front of the background image

            // 3. Recipe Photo
            PictureBox pbPhoto = new PictureBox
            {
                Image = (Image)Properties.Resources.ResourceManager.GetObject(_currentRecipe.RecipeImageResourceName),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 300,
                Height = 300,
                Location = new Point(40, 100),
                BorderStyle = BorderStyle.FixedSingle
            };
            // Fallback check (Important for robust loading)
            if (pbPhoto.Image == null)
            {
                pbPhoto.Image = Properties.Resources.CakePhoto;
            }
            this.Controls.Add(pbPhoto);

            // 4. Ingredients Display (RichTextBox for numbered list)
            Label lblIngredients = new Label
            {
                Text = "Required Ingredients:",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                Location = new Point(360, 100),
                AutoSize = true
            };
            this.Controls.Add(lblIngredients);

            RichTextBox rtbIngredients = new RichTextBox
            {
                Text = _currentRecipe.GetIngredientsDisplay(),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.White,
                ForeColor = Color.Black,
                Width = 380,
                Height = 250,
                Location = new Point(360, 130),
                ReadOnly = true,
                BorderStyle = BorderStyle.None
            };
            this.Controls.Add(rtbIngredients);

            // 5. Special Instruction / Chef's Tip
            Label lblInstructionHeader = new Label
            {
                Text = "👨‍🍳 Chef's Tip:",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Brown, // Highlight the tip
                BackColor = Color.Transparent,
                Location = new Point(40, 420),
                AutoSize = true
            };
            this.Controls.Add(lblInstructionHeader);

            Label lblInstruction = new Label
            {
                Text = _currentRecipe.SpecialInstruction,
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                ForeColor = Color.LightGray,
                BackColor = Color.FromArgb(150, 0, 0, 0),
                Width = 720,
                Height = 60,
                Location = new Point(40, 450),
                Padding = new Padding(10),
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(lblInstruction);

            // 6. Video Player (WebBrowser)
            Label lblVideo = new Label
            {
                Text = "▶️ How-To Video Guide:",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Brown,
                BackColor = Color.Transparent,
                Location = new Point(40, 530),
                AutoSize = true
            };
            this.Controls.Add(lblVideo);

            WebBrowser videoBrowser = new WebBrowser
            {
                Width = 720,
                Height = 250,
                Location = new Point(40, 560),
                ScrollBarsEnabled = false
            };

            // Generate the HTML content to embed the YouTube video
            if (!string.IsNullOrEmpty(_currentRecipe.VideoUrl))
            {
                // Create a simple, responsive HTML wrapper for the video iframe
                string html = $@"
                    <html>
                    <body style='margin: 0; background-color: #333;'>
                    <div style='position: relative; width: 100%; height: 100%;'>
                    <iframe 
                        width='100%' 
                        height='100%' 
                        src='{_currentRecipe.VideoUrl}' 
                        frameborder='0' 
                        allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture' 
                        allowfullscreen>
                    </iframe>
                    </div>
                    </body>
                    </html>";
                videoBrowser.DocumentText = html;
            }
            this.Controls.Add(videoBrowser);
        }

        // --- Event Handler ---
        private void BackButton_Click(object sender, EventArgs e)
        {
            _parentForm.Show();
            this.Close();
        }

        private void RecipeDetailForm_Load(object sender, EventArgs e)
        {
            // initialization code here
        }
    }
}