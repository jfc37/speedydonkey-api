Feature: CreateRoom

@settings @create @logo @golden_path
Scenario: Set logo url setting
	Given a valid logo url is ready to be submitted
	When the settings are attempted to be set
	Then logo setting is retrieved

@settings @create @logo @validation_error
Scenario: Try to set up an invalid logo url setting
	Given an invalid logo url is ready to be submitted
	When the settings are attempted to be set
	Then validation errors are returned
	Then logo setting is not retrieved
