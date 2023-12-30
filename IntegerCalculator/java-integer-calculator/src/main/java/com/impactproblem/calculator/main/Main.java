package com.impactproblem.calculator.main;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.web.servlet.support.SpringBootServletInitializer;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
@ComponentScan({"com.impactproblem.calculator"})
public class Main extends SpringBootServletInitializer {
	
	public static void main(String[] args) {
		SpringApplication.run(Main.class, args);
	}
}
