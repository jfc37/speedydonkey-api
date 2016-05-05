Feature: StandAloneEventRegistration

@stand_alone_event @registration
Scenario: User registers for stand alone event
Given a stand alone event exists
When the user registers for the stand alone event
Then the user is registered in the stand alone event
And the user has an item in their upcoming schedule

@stand_alone_event @show
Scenario: User looks at stand alone events to register for
Given a stand alone event exists
When upcoming stand alone events are requested
Then the request is successful
And the student sees the stand alone event
And the student is not marked as already attending

@stand_alone_event @show
Scenario: User looks at stand alone events that they are already registered for
Given the user is registered for a stand alone event
When upcoming stand alone events are requested
Then the request is successful
And the student sees the stand alone event
And the student is marked as already attending