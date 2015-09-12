Feature: CreateBlock

@block @create @golden_path
Scenario: Generate a block
	Given a level exists
	When a block is generated from the level
	Then block can be retrieved