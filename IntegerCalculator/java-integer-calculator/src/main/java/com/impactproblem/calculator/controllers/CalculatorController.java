package com.impactproblem.calculator.controllers;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

import com.google.gson.Gson;
import com.impactproblem.calculator.services.CalculatorService;

@RestController
public class CalculatorController {
	@Autowired
	private CalculatorService calculatorService;

	@CrossOrigin
	@GetMapping("/add/{firstNumber}/{secondNumber}")
	public String addNumbers(@PathVariable Integer firstNumber, @PathVariable Integer secondNumber) {

		Gson gson = new Gson();

		Map<String, Integer> number = new HashMap<>();
		number.put("number", (calculatorService.add(firstNumber, secondNumber)));
		return gson.toJson(number);
	}

	@CrossOrigin
	@GetMapping("/subtract/{firstNumber}/{secondNumber}")
	public String subtractNumbers(@PathVariable Integer firstNumber, @PathVariable Integer secondNumber) {

		Gson gson = new Gson();

		Map<String, Integer> number = new HashMap<>();
		number.put("number", (calculatorService.subtract(firstNumber, secondNumber)));
		return gson.toJson(number);
	}

	@CrossOrigin
	@GetMapping("/multiply/{firstNumber}/{secondNumber}")
	public String multiplyNumbers(@PathVariable Integer firstNumber, @PathVariable Integer secondNumber) {

		Gson gson = new Gson();

		Map<String, Integer> number = new HashMap<>();
		number.put("number", (calculatorService.multiply(firstNumber, secondNumber)));
		return gson.toJson(number);
	}
}
