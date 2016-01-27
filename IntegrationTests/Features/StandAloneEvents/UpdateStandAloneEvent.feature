Feature: UpdateStandAloneEvent

@stand_alone_event @update @golden_path
Scenario: Update stand alone event
	Given a stand alone event exists
	And the stand alone event needs to be changed to private
	When the stand alone event is updated
	Then the request is successful
	And the stand alone event is now private