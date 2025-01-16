Feature: Square Root
	As Alice (the customer)
	I want to know the square root of a number
	So I can stimate sales in a certain date


Scenario Outline: Square Root of a number with exact square root
	Given a number like 25
	Calculate its square root
	Then the result should be 5
	Examples: 
	| number | result  |
	| 1      | 1       |
	| 4      | 2       |
	| 9      | 3       |
	| 16     | 4       |
	| 25     | 5       |
	| 36     | 6       |
	| 121    | 11      |


Scenario Outline: Square Root of a number with decimal square root
	Given a number like 2
	Calculate its square root
	Then the result should be 1.41
	Examples: 
	| number | result  |
	| 2      | 1.41    |
	| 3      | 1.73    |
	| 5      | 2.23    |
	| 20     | 4.47    |