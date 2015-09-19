Feature: CreateLevel
	In order to be able to create blocks back to back
	As an admin
	I want to create a level

@level @create @golden_path
Scenario: Create a level
	Given a valid level is ready to be submitted
	When the level is attempted to be created
	Then level can be retrieved

@level @create @validation_error
Scenario: Try to set up an invalid level
	Given an invalid level is ready to be submitted
	When the level is attempted to be created
	Then validation errors are returned
	And level can not be retrieved