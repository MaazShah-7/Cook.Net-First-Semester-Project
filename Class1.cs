namespace CookNetLibrary                    // Our Namespace for the Class Library //
    {
        /*------------------------------------------------------------------------------------------------- */
        /*                                            RECIPE DATA MODEL                                     */
        /*--------------------------------------------------------------------------------------------------*/

        public class Recipe
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public List<string> Ingredients { get; set; }
            public string SpecialInstruction { get; set; }
            public string VideoUrl { get; set; }
            public string RecipeImageResourceName { get; set; }


            //These all were added to hold recipe data including optional video URL//

            public Recipe(string name, string category, List<string> ingredients, string instruction = "N/A", string videoUrl = "", string imageResource = "DefaultRecipePhoto")
            {
                Name = name; Category = category; Ingredients = ingredients; SpecialInstruction = instruction; VideoUrl = videoUrl; RecipeImageResourceName = imageResource;
            }

            public string GetIngredientsDisplay() { return string.Join(Environment.NewLine, Ingredients.Select((item, index) => $"{index + 1}. {item}")); }

            // Method to format ingredients for display //
        }

        /*------------------------------------------------------------------------------------------------- */
        /*                                               RECIPE MANAGER                                     */
        /*--------------------------------------------------------------------------------------------------*/

        public class RecipeManager
        {
            public RecipeManager() { }
            // Getting All Categories //
            public static List<string> GetCategories()
            {
                return
                [
                "Meal",
                "Drink",
                "Dessert"
                ];
            }
            // Getting Recipes by Category //
            public static List<string> GetNameOfRecipe(string category)
            {
                if (category == "Meal")
                    return ["Biryani", "Spaghetti", "Tikka"];
                else if (category == "Drink")
                    return ["Coffee", "Shake", "Juice"];
                else if (category == "Dessert")
                    return ["PanCake", "Brownie", "Kheer"];
                else
                    return [];
            }
            // Getting Recipe Details by Name //
            public List<Recipe> GetRecipeInfo(string name)
            {
                // STORING VIDEO EMBED LINKS //

             //   string BiryaniVideoEmbed //= "#"; 
                string TikkaVideoEmbed = "https://www.youtube.com/watch?v=5EAgEwjHaSk&list=PLNvPVY82SXkbfS14q5pKikJiizhPY1tmr&index=2&pp=gAQBiAQB";
                string SpaghettiVideoEmbed = "https://www.youtube.com/watch?v=OAmzdY32uQ8&list=PLNvPVY82SXkbfS14q5pKikJiizhPY1tmr&index=3&pp=gAQBiAQB";
                string PanCakeVideoEmbed = "https://www.youtube.com/watch?v=k6i2t2OPO-8&list=PLNvPVY82SXkbfS14q5pKikJiizhPY1tmr&index=4&pp=gAQBiAQB";
                string BrownieVideoEmbed = "https://www.youtube.com/watch?v=uNsdP-md3MA&list=PLNvPVY82SXkbfS14q5pKikJiizhPY1tmr&index=6&pp=gAQBiAQB";
                string KheerVideoEmbed = "https://www.youtube.com/watch?v=k6i2t2OPO-8&list=PLNvPVY82SXkbfS14q5pKikJiizhPY1tmr&index=4&pp=gAQBiAQB";
                string CoffeeVideoEmbed = "https://www.youtube.com/watch?v=xA_-m4EMjvs&list=PLNvPVY82SXkbfS14q5pKikJiizhPY1tmr&index=8&pp=gAQBiAQB";
                string ShakeVideoEmbed = "https://www.youtube.com/watch?v=4mX-tz5kbrk&list=PLNvPVY82SXkbfS14q5pKikJiizhPY1tmr&index=7&pp=gAQBiAQB";
                string JuiceVideoEmbed = "https://www.youtube.com/watch?v=fiJ0kT2qnCQ&list=PLNvPVY82SXkbfS14q5pKikJiizhPY1tmr&index=9&pp=gAQBiAQB0gcJCQMKAYcqIYzv";

                switch (name)
                {
                    case "Biryani":
                        return
                        [
                            new (
                            "Biryani",
                            "Meal",
                            [
                                " Basmati Rice",
                                " Chicken/Mutton",
                                " Yogurt",
                                " Onion",
                                " Tomato",
                                " Ginger-Garlic Paste",
                                " Biryani Masala",
                                " Saffron",
                                " Oil/Ghee"
                            ],
                            "After layering the rice and meat mixture, seal the pot tightly with aluminum foil before placing the lid to achieve the perfect \"dum\" (steam cooking).",
                            "https://www.youtube.com/watch?v=uFa6urPkK4c&list=PLNvPVY82SXkbfS14q5pKikJiizhPY1tmr&index=1&t=11s&pp=gAQBiAQB", "BiryaniPhoto"
                        )
                        ];


                    case "Spaghetti":
                        return
                        [
                            new Recipe(
                            "Spaghetti",
                            "Meal",
                            [
                                " Spaghetti Pasta",
                                " Ground Meat (Beef/Pork/Turkey)",
                                " Canned Crushed Tomatoes",
                                " Onion",
                                " Garlic",
                                " Olive Oil",
                                " Basil",
                                " Oregano",
                                " Salt",
                                " Pepper"
                            ],
                            "Reserve about 1 cup of the starchy pasta water before draining; add it to the sauce to help it emulsify and cling better to the pasta.",
                            SpaghettiVideoEmbed, "SpaghettiPhoto"
                        )
                        ];


                    case "Tikka":
                        return
                        [
                            new Recipe(
                            "Tikka",
                            "Meal",
                            [
                                " Chicken/Paneer Cubes",
                                " Yogurt",
                                " Ginger-Garlic Paste",
                                " Lemon Juice",
                                " Tikka Masala Powder",
                                " Turmeric",
                                " Red Chili Powder",
                                " Mustard Oil"
                            ],
                            "For a smoky flavor without a tandoor, place a small, hot piece of charcoal in the marinade, drizzle with ghee, and cover the bowl for 5 minutes (dhungar).",
                            TikkaVideoEmbed, "TikkaPhoto"
                        )
                        ];


                    case "Coffee":
                        return
                        [
                            new Recipe(
                            "Coffee",
                            "Drink",
                            [
                                " Coffee Beans/Grounds",
                                " Water (filtered)",
                                " Sugar (optional)",
                                " Milk/Cream (optional)"
                            ],
                            "Use water that is just off the boil (195°F–205°F or 90°C–96°C); boiling water can \"burn\" the coffee grounds and create a bitter taste.",
                            CoffeeVideoEmbed, "CoffeePhoto"
                        )
                        ];


                    case "Shake":
                        return
                        [
                            new Recipe(
                            "Shake",
                            "Drink",
                            [
                                " Ice Cream",
                                " Milk",
                                " Flavoring (e.g., Chocolate Syrup, Fruit)",
                                " Ice Cubes (optional)"
                            ],
                            "For a thicker shake without using excessive ice cream, use frozen fruit (like bananas or berries) instead of plain ice cubes.",
                            ShakeVideoEmbed, "ShakePhoto"
                        )
                        ];


                    case "Juice":
                        return
                        [
                            new Recipe(
                            "Juice",
                            "Drink",
                            [
                                " Fresh Fruit/Vegetable (e.g., Oranges, Carrots)",
                                " Water (optional)",
                                " Sugar/Sweetener (optional)",
                                " Ice Cubes"
                            ],
                            "To maximize juice extraction and preserve nutrients, ensure the fruit/vegetables are cold before juicing.",
                            JuiceVideoEmbed, "JuicePhoto"
                        )
                        ];


                    case "PanCake":
                        return
                        [
                            new Recipe(
                            "PanCake",
                            "Dessert",
                            [
                                " All-Purpose Flour",
                                " Milk",
                                " Egg",
                                " Baking Powder",
                                " Sugar",
                                " Salt",
                                " Butter (for cooking)"
                            ],
                            "Do not overmix the batter; lumps are okay. Overmixing develops gluten and results in tough, flat pancakes.",
                            PanCakeVideoEmbed, "PanCakePhoto"
                        )
                        ];


                    case "Brownie":
                        return
                        [
                            new Recipe(
                            "Brownie",
                            "Dessert",
                            [
                                " Unsweetened Cocoa Powder",
                                " All-Purpose Flour",
                                " Sugar",
                                " Eggs",
                                " Butter",
                                " Baking Powder",
                                " Vanilla Extract",
                                " Salt",
                                " Chocolate Chips"
                            ],
                            "To achieve a shiny, crinkly top crust, ensure the sugar is fully dissolved into the melted butter and eggs before adding the dry ingredients.",
                            BrownieVideoEmbed, "BrowniePhoto"
                        )
                        ];


                    case "Kheer":
                        return
                        [
                            new Recipe(
                            "Kheer",
                            "Dessert",
                            [
                                " Rice",
                                " Milk (full fat recommended)",
                                " Sugar",
                                " Cardamom Pods (Elaichi)",
                                " Saffron Strands",
                                " Mixed Nuts (Pistachios, Almonds)",
                                " Raisins/Dried Fruit"
                            ],
                            "Cook the Kheer on a low flame and stir frequently, especially towards the end, to prevent the milk from scalding and sticking to the bottom of the pot.",
                            KheerVideoEmbed, "KheerPhoto"
                        )
                        ];

                    default:
                        return [];
                }
            }

            // Getting File Path for Recipe Image //
            public static string GetRecipeImagePath(string name)
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string imagePath = System.IO.Path.Combine(basePath, "Images", $"{name}.jpg");
                switch (name)
                {
                    case "Biryani":
                        return @"Images\biryani.jpg";
                    case "Spaghetti":
                        return @"Images\spaghetti.jpg";
                    case "Tikka":
                        return @"Images\tikka.jpg";
                    case "Coffee":
                        return @"Images\coffee.jpg";
                    case "Shake":
                        return @"Images\shake.jpg";
                    case "Juice":
                        return @"Images\juice.jpg";
                    case "PanCake":
                        return @"Images\PanCake.jpg";
                    case "Brownie":
                        return @"Images\brownie.jpg";
                    case "Kheer":
                        return @"Images\kheer.jpg";
                    default:
                        return @"Images\default.jpg";
                }
            }
        }
}
