Feature: CreateSetting

@settings @create @terms_and_conditions @golden_path
Scenario: Set terms and a conditions setting
	Given a valid terms and conditions is ready to be submitted
	When the settings are attempted to be set
	Then terms and conditions setting is retrieved

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
	
@settings @update @logo @golden_path
Scenario: Update logo url setting
	Given the logo setting is already set
	And the logo setting needs to be changed
	When the settings are attempted to be set
	Then logo setting is retrieved