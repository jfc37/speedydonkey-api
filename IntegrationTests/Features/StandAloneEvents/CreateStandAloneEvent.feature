Feature: CreateStandAloneEvent

@stand_alone_event @create @golden_path
Scenario: Create stand alone event
	Given a valid stand alone event is ready to be submitted
	When the stand alone event is attempted to be created
	Then the request is successful
	And the stand alone event can be retrieved
	
@stand_alone_event @create @validation_error
Scenario: Try to set up an invalid stand alone event
	Given an invalid stand alone event is ready to be submitted
	When the stand alone event is attempted to be created
	Then validation errors are returned
	And the request is unsuccessful