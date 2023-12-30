package com.impactproblem.calculator.services;

import org.springframework.stereotype.Service;

@Service
public class CalculatorService {
	
	public Integer add(Integer firstNumber, Integer secondNumber) {
		
		return firstNumber + secondNumber;
	}
	
	public Integer subtract(Integer firstNumber, Integer secondNumber) {
		
		return firstNumber - secondNumber;
	}
	
	public Integer multiply(Integer firstNumber, Integer secondNumber) {
		
		return firstNumber * secondNumber;
	}
}
