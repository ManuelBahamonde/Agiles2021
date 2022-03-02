Feature: Game
	In order to play the game
	As a player
	I want to guess a word and know whether I won or not

Scenario: Hit the Secret Word
	Given I have started a new game on Easy difficulty
	When I try Puma as the secret word
	Then I should be told that I win

Scenario: Hit a letter
	Given I have started a new game on Easy difficulty
	When I try P as a letter of the secret word 1 times
	Then I should see P in the result

Scenario: Lose the game
	Given I have started a new game on Easy difficulty
	When I try Z as a letter of the secret word 6 times
	Then I should be told that I lost

Scenario: Risk a special character
	Given I have started a new game on Easy difficulty
	When I try ! as a letter of the secret word 1 times
	Then I should be told that the letter is invalid

Scenario: Risk a non-alphabetic Secret Word
	Given I have started a new game on Easy difficulty
	When I try Puma123 as the secret word
	Then I should be told that the word is invalid