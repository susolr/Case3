Feature: PrimeNumber
	As Alice (the customer)
	I want to know whether a number is prime or not
	So I can create discount campaings


Scenario Outline: Checking several prime numbers
	When number <number> is checked
	Then the answer to know whether is prime or not is <result>
	Examples: 
	| number | result  |
	| 2      | Yes     |
	| 3      | Yes     |
	| 5      | Yes     |
	| 7      | Yes     |
	| 11     | Yes     |
	| 997    | Yes     |
	| 98689  | Yes     |
	| 86743  | Yes     |