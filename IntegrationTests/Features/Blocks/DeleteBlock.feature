Feature: DeleteBlock

@block @delete @golden_path
Scenario: Delete block
	Given a block exists
	And the current user enrols in the block
	When the block is deleted
	And the current user schedule is retrieved
	Then the block can not be retrieved
	And the current user's schedule is emtpy
