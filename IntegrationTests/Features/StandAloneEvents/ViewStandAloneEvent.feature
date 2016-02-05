Feature: ViewStandAloneEvent

@stand_alone_event @private_event @create @golden_path
Scenario: Private event is not available for registration
	Given a private stand alone event exists
	When upcoming stand alone events are requested
	Then the request returns not found

@stand_alone_event @create @golden_path
Scenario: Public event is available for registration
	Given a stand alone event exists
	When upcoming stand alone events are requested
	Then the request is successful