Feature: Square Root
  As Alice (the customer)
  I want to know the square root of a number
  So I can estimate sales on a certain date

  Scenario Outline: Square Root of a number with exact square root
    Given the number <number>
    When number <number> is checked for its square root
    Then the answer to the square root is <result>
    Examples:
      | number | result |
      | 1      | 1      |
      | 4      | 2      |
      | 9      | 3      |
      | 16     | 4      |
      | 25     | 5      |
      | 36     | 6      |
      | 121    | 11     |

  Scenario Outline: Square Root of a number with decimal square root
    Given the number <number>
    When number <number> is checked for its square root
    Then the answer to the square root is <result>
    Examples:
      | number | result |
      | 2      | 1.41   |
      | 3      | 1.73   |
      | 5      | 2.23   |
      | 20     | 4.47   |
