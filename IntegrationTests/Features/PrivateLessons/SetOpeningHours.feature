Feature: SetOpeningHours

@opening_hours @create @golden_path
Scenario: Set opening hours
	Given a valid opening hour is ready to be submitted
	When the opening hour is attempted to be created
	Then the request is successful
	And the opening hour can be retrieved

@opening_hours @create @validation_errors
Scenario: Set invalid opening hours
	Given a invalid opening hour is ready to be submitted
	When the opening hour is attempted to be created
	Then the request is unsuccessful
	And the opening hour cannot be retrieved

@opening_hours @update @golden_path
Scenario: Update opening hours
	Given opening hours are set
	And opening hours need to be changed
	When the opening hour is attempted to be updated
	Then the request is successful
	And the opening hour can be retrieved
	