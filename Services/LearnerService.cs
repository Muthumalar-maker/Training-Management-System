using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class LearnerService
    {
        // Store learners using Learner ID as the key
        private Dictionary<string, Learner> learners =
            new Dictionary<string, Learner>(
                StringComparer.OrdinalIgnoreCase);

        // Check whether a learner exists
        public bool LearnerExists(string learnerId)
        {
            return learners.ContainsKey(learnerId);
        }

        // Register a learner
        public void RegisterLearner()
        {
            Console.WriteLine("\n--- Learner Registration ---");

            Console.Write("Enter Learner ID: ");
            string id = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Learner ID cannot be empty.");
                return;
            }

            if (learners.ContainsKey(id))
            {
                Console.WriteLine("Learner ID already exists.");
                return;
            }

            Console.Write("Enter Name: ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            Console.Write("Enter Contact Number: ");
            string contact = (Console.ReadLine() ?? "").Trim();

            // Validate a 10-digit Indian mobile number
            if (!Regex.IsMatch(contact, @"^[6-9][0-9]{9}$"))
            {
                Console.WriteLine(
                    "Invalid contact. Enter a valid 10-digit Indian mobile number.");
                return;
            }

            Console.Write("Enter Email: ");
            string email = (Console.ReadLine() ?? "").Trim();

            // Validate email
            if (string.IsNullOrWhiteSpace(email) ||
                !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Console.WriteLine("Invalid email address.");
                return;
            }

            Learner learner = new Learner(
                id, name, contact, email);

            learners.Add(id, learner);

            Console.WriteLine("Learner registered successfully!");
        }

        // Display all learners
        public void DisplayAllLearners()
        {
            Console.WriteLine("\n--- Registered Learners ---");

            if (learners.Count == 0)
            {
                Console.WriteLine("No learners registered.");
                return;
            }

            foreach (Learner learner in learners.Values)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("Learner ID: " + learner.LearnerId);
                Console.WriteLine("Name: " + learner.Name);
                Console.WriteLine("Contact: " + learner.ContactNumber);
                Console.WriteLine("Email: " + learner.Email);
            }
        }

        // Search learner by ID
        public void SearchLearner()
        {
            Console.Write("Enter Learner ID to search: ");
            string id = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Learner ID cannot be empty.");
                return;
            }

            if (learners.TryGetValue(id, out Learner? learner)
                && learner != null)
            {
                Console.WriteLine("\nLearner Found!");
                Console.WriteLine("ID: " + learner.LearnerId);
                Console.WriteLine("Name: " + learner.Name);
                Console.WriteLine("Contact: " + learner.ContactNumber);
                Console.WriteLine("Email: " + learner.Email);
            }
            else
            {
                Console.WriteLine("Learner not found.");
            }
        }

        // Remove learner by ID
        public void RemoveLearner()
        {
            Console.Write("Enter Learner ID to remove: ");
            string id = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Learner ID cannot be empty.");
                return;
            }

            if (learners.Remove(id))
            {
                Console.WriteLine("Learner removed successfully.");
            }
            else
            {
                Console.WriteLine("Learner not found.");
            }
        }
    }
}
