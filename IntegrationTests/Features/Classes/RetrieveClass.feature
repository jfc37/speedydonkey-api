Feature: RetrieveClass

@class @retrieve @get_all
Scenario: Get all classes
	Given a block exists
	When all classes are retreived
	Then something is retreived

@class @retrieve @search
Scenario: Search classes
	Given a block exists
	When a class search is performed
	Then something is retreived

@class @retrieve @get_by_id
Scenario: Get class by id
	Given a block exists
	When a class is retrieved by id
	Then something is retreived