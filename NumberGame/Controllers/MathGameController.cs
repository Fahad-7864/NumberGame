using Microsoft.AspNetCore.Mvc;
using NumberGame.Models;
using System;
using System.Collections.Generic;

public class MathGameController : Controller
{
    private static Random _random = new Random();

    public IActionResult Index()
    {
        var game = GenerateMathGame();
        return View(game);
    }

    private MathGame GenerateMathGame()
    {
        // Generate two random numbers between 0 and 100
        int number1 = _random.Next(0, 101);
        int number2 = _random.Next(0, 101);

        // Calculate the target result
        int targetResult = number1 + number2;

        // Create a list of random numbers including the correct ones
        var numbers = new List<int>
        {
            number1,
            number2,
            _random.Next(0, 101),
            _random.Next(0, 101),
            _random.Next(0, 101)
        };

        // Shuffle the list so the correct answers aren't always at the same position
        Shuffle(numbers);

        // Return the game object with the operation and generated numbers
        var game = new MathGame
        {
            Operation = "+",
            TargetResult = targetResult,
            Numbers = numbers
        };

        return game;
    }

    // Method to shuffle the list
    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = _random.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    [HttpPost]
    public IActionResult CheckAnswer(int[] selectedNumbers, int targetResult, string operation)
    {
        bool isCorrect = false;

        // Ensure exactly two numbers are selected
        if (selectedNumbers.Length == 2 && operation == "+")
        {
            // Check if the sum of the two selected numbers equals the target result
            isCorrect = (selectedNumbers[0] + selectedNumbers[1] == targetResult);
        }

        ViewBag.IsCorrect = isCorrect;
        return View("Index", GenerateMathGame());  // Present a new problem after checking the answer
    }

}
