using System;
using System.Drawing;
using System.Windows.Forms;
using CookNetLibrary;

namespace CookNet // <-- MUST MATCH YOUR PROJECT NAME
{
    // The 'partial' keyword is essential to link this file with Form1.Designer.cs
    public partial class Form1 : Form
    {
        private RecipeManager _recipeManager = new RecipeManager();

        public Form1()
        {
            // This line calls the UI setup method defined in Form1.Designer.cs.
            InitializeComponent();

            this.Text = "COOK.NET - Select Category";
            this.Size = new Size(500, 460);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            SetupCategoryUI(); // <-- Moved inside the constructor block
        }
        // --- NEW: Add this empty method to satisfy the Designer file ---
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // Set up the aesthetic and layout of the main screen

        // --- UI Setup Method ---
        private void SetupCategoryUI()
        {
            this.BackgroundImage = Properties.Resources.KitchenBackground; // Load the main background image
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // 1. Panel for Banner (Top Section)
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(40, 40, 40) // Dark background for contrast
            };

            // Remove panel padding so the banner can touch the sides
            pnlHeader.Padding = new Padding(0);

            this.Controls.Add(pnlHeader);

            PictureBox pbBanner = new PictureBox
            {
                Image = Properties.Resources.AppLogoBanner,
                // Fill the entire panel so the banner touches the left/right edges
                Dock = DockStyle.Fill,
                // Choose SizeMode:
                // - StretchImage will force the image to fill the area (may distort)
                // - Zoom will preserve aspect ratio and center the image (may leave small gaps)
                SizeMode = PictureBoxSizeMode.StretchImage,
                Margin = Padding.Empty
            };
            pnlHeader.Controls.Add(pbBanner);

            // 2. Panel for Buttons (Center Section)
            FlowLayoutPanel pnlButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                WrapContents = false,
                Padding = new Padding(50, 40, 50, 20),
                BackColor = Color.Transparent
            };
            this.Controls.Add(pnlButtons);
            pnlButtons.BringToFront(); // Ensure buttons are above the background

            // Add Category Buttons
            CreateCategoryButton(pnlButtons, "Meal", Properties.Resources.IconMeal);
            CreateCategoryButton(pnlButtons, "Dessert", Properties.Resources.IconDessert);
            CreateCategoryButton(pnlButtons, "Drink", Properties.Resources.IconDrink);
        }

        // --- Helper to Create Buttons ---
        private void CreateCategoryButton(FlowLayoutPanel container, string text, Image icon)
        {
            Button btn = new Button
            {
                Text = text,
                Image = icon,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial MT", 16, FontStyle.Bold),
                Size = new Size(350, 70),
                Margin = new Padding(10),
                Tag = text, // Store the category name in the Tag
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += CategoryButton_Click;
            container.Controls.Add(btn);
        }

        // --- Event Handler ---
        private void CategoryButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string selectedCategory = clickedButton.Tag.ToString();

            // Navigate to the next form (Recipe List)
            RecipeListForm listForm = new RecipeListForm(selectedCategory, this);
            listForm.Show();
            this.Hide();
        }
    }
}
