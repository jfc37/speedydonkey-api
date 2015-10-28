Feature: DeleteClass

@class @delete @golden_path
Scenario: Delete class
	Given a block exists
	And the current user enrols in the block
	And a class needs to be deleted
	When the class is deleted
	And the current user schedule is retrieved
	Then the class can not be retrieved
	And the current user's schedule is emtpy

@class @delete @validation_error
Scenario: Fails to delete class that has attendance
	Given a block exists
	And the current user enrols in the block
	And a class needs to be deleted
	But a user has attended the class
	When the class is deleted
	And the current user schedule is retrieved
	Then validation errors are returned
	And the class can be retrieved
	And the current user's schedule is not emtpy
