Feature: UpdateStandAloneEvent

@stand_alone_event @update @golden_path
Scenario: Update stand alone event
	Given a stand alone event exists
	And the stand alone event needs to be changed to private
	When the stand alone event is updated
	Then the request is successful
	And the stand alone event is now private

@stand_alone_event @update @golden_path
Scenario: Update stand alone event class capacity
	Given a stand alone event exists
	And the stand alone event class capacity changes to '40'
	When the stand alone event is updated
	Then the request is successful
	And the stand alone event class capacity is '40'

	Given a block exists
	And the block class capacity changes to '40'
	When the block is updated
	Then the block class capacity is '40'