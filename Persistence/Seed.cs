using Domain;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

namespace Persistence;

public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any() && !context.ListTasks.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Name = "Bob_Brown",
                        Email = "Bob-Brown@mail.com"
                    },
                    
                    new AppUser
                    {
                        Name = "Jane_Yellow",
                        Email = "Jane_Yellow@list.com"
                    },
                    
                    new AppUser
                    {
                        Name = "Tom_White",
                        Email = "Tom_White@gmail.com"
                    },
                };
                
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                var listTask = new List<ListTask>
                {
                    new ListTask
                    {
                        AppUser = users[0],
                        Name = "Working Tasks",
                        Description = "Job tasks are duties or responsibilities that you perform on a job. " +
                                      "Most workers perform numerous tasks on their jobs. " +
                                      "For example, a secretary may arrange meetings, type letters and run errands for her boss.",
                        
                        Tasks = new List<Tasks>
                        {
                            new Tasks
                            {
                                Name = "Write Article",
                                Description = "Article insteristing",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            },
                            new Tasks
                            {
                                Name = "Clean Laptop",
                                Description = "Laptop working",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            },
                            new Tasks
                            {
                                Name = "Save all files",
                                Description = "Files determinations",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            }
                        }
                    },
                    new ListTask
                    {
                        AppUser = users[0],
                        Name = "Home Tasks",
                        Description = "The complete household chores list. " +
                                      "Without a plan for keeping your house clean, it can seem like the to-do list is endless: " +
                                      "the spice rack is impossible to navigate, the towels constantly need to be washed, " +
                                      "there’s a weird stain in the cupboard by the stove.",
                        
                        Tasks = new List<Tasks>
                        {
                            new Tasks
                            {
                                Name = "Clean Windows",
                                Description = "Windows are in brown rust",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            },
                            new Tasks
                            {
                                Name = "Clean Clothes",
                                Description = "Clothes are dirty",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            },
                            new Tasks
                            {
                                Name = "Cook borsh",
                                Description = "borsh is red soup which popular in Russia",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            }
                        }
                    },
                    new ListTask
                    {
                        AppUser = users[1],
                        Name = "Jym Tasks",
                        Description = "Where the gym is a very ‘each to their own‘ environment, " +
                                      "everyone has there own go-to exercises, routines and set ups. " +
                                      "When it comes to exercises in your routine , some will always be advantageous, " +
                                      "no matter what your approach. These 10 exercises should have a place in most gym goers plan…",
                        
                        Tasks = new List<Tasks>
                        {
                            new Tasks
                            {
                                Name = "Plank",
                                Description = "Often overlooked, but core exercises have a great carry over to nearly all",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            },
                            new Tasks
                            {
                                Name = "Squats",
                                Description = "Whether it’s barbell, dumbbell or goblet style to ease the spinal compression",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            },
                            new Tasks
                            {
                                Name = "Bent Over Rows",
                                Description = "One of the best back exercises you can do!",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            }
                        }
                    },
                    new ListTask
                    {
                        AppUser = users[1],
                        Name = "Yoga Tasks",
                        Description = "As a new yoga student, you might feel overwhelmed by the number " +
                                      "of poses and their odd-sounding names. But yoga doesn't have to be complicated.",
                        
                        Tasks = new List<Tasks>
                        {
                            new Tasks
                            {
                                Name = "Standing poses",
                                Description = "Standing poses are often done first in a yoga class",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            },
                            new Tasks
                            {
                                Name = "Balancing poses",
                                Description = "Beginners' balances are an important way to build the core",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            },
                            new Tasks
                            {
                                Name = "Backbends",
                                Description = "As a beginner, you will generally begin with gentle spine",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            }
                        }
                    },
                    new ListTask
                    {
                        AppUser = users[2],
                        Name = "Driving Tasks",
                        Description = "Long car rides can be uncomfortable, causing aches and pains. " +
                                      "Help your patients stay comfortable and active during long road trips whether " +
                                      "they’re visiting relatives or going on vacation.",
                        
                        Tasks = new List<Tasks>
                        {
                            new Tasks
                            {
                                Name = "Seat Pushes and Tricep Pushes",
                                Description = "If you are the driver, seat pushes can help exercise your triceps.",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            },
                            new Tasks
                            {
                                Name = "Seated Side Bend",
                                Description = "Sit tall, with your hands behind your head and your fingers clasped.",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            },
                            new Tasks
                            {
                                Name = "Calf Raises",
                                Description = "Position both of your feet flat on the floor.",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            }
                        }
                    },
                    new ListTask
                    {
                        AppUser = users[2],
                        Name = "Relaxation Tasks",
                        Description = "Relaxation techniques can reduce stress symptoms and " +
                                      "help you enjoy a better quality of life, especially if " +
                                      "you have an illness. Explore relaxation techniques you can do by yourself.",
                        
                        Tasks = new List<Tasks>
                        {
                            new Tasks
                            {
                                Name = "Slowing heart rate",
                                Description = "When faced with many responsibilities",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            },
                            new Tasks
                            {
                                Name = "Lowering blood pressure",
                                Description = "But that means you might miss out on the health benefits of relaxation.",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            },
                            new Tasks
                            {
                                Name = "Slowing breathing rate",
                                Description = "Health care providers such as complementary and integrative health specialists and mental health providers can teach many relaxation techniques.",
                                CreatedAt = DateTime.UtcNow,
                                Status = 0
                            }
                        }
                    },
                    
                };

                await context.ListTasks.AddRangeAsync(listTask);
                await context.SaveChangesAsync();
            }
        }
    }
