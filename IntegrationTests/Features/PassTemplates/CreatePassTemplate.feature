Feature: CreatePassTemplate

@passtemplate @create @golden_path
Scenario: Create a pass template
	Given a valid pass template is ready to be submitted
	When the pass template is attempted to be created
	Then pass template can be retrieved

@passtemplate @create @validation_error
Scenario: Try to set up an invalid pass template
	Given an invalid pass template is ready to be submitted
	When the pass template is attempted to be created
	Then validation errors are returned
	And pass template can not be retrieved
