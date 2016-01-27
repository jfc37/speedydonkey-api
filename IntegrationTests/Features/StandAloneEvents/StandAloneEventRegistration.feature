Feature: StandAloneEventRegistration

@stand_alone_event @registration
Scenario: User registers for stand alone event
Given a stand alone event exists
When the user registers for the stand alone event
Then the user is registered in the stand alone event
And the user has an item in their upcoming schedule