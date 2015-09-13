Feature: RetrieveLevel

@level @retrieve @get_all
Scenario: Get all levels
	Given a level exists
	When all levels are retreived
	Then something is retreived

@level @retrieve @search
Scenario: Search levels
	Given a level exists
	When a level search is performed
	Then something is retreived

@level @retrieve @search
Scenario: Get level by id
	Given a level exists
	When a level is retrieved by id
	Then something is retreived

