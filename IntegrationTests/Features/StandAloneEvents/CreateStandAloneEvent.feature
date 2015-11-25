Feature: CreateStandAloneEvent

@stand_alone_event @create @golden_path
Scenario: Create stand alone event
	Given a valid stand alone event is ready to be submitted
	When the stand alone event is attempted to be created
	Then the stand alone event can be retrieved
