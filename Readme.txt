http://blog.petegoo.com/index.php/2011/09/02/building-an-enterprise-workflow-system-with-wf4/

The basic principle here is an Enterprise Workflow system where we can have authors create an implementations of a
Magic Eight Ball service and a web site will allow users to pick an implementation, ask it a question, give it their
email address and have it send them a response.

The structure of this Solution is as follows

- Solution
  - StandardWorkflowService: This is a sample of an EightBall implemented with the ou-of-the-box WF Service template
  - Enterprise Workflow
    - EightBalls: The various implementations of Magic Eight Balls in our enterprise WF system
	- EightBallLibrary: The parts that allow hosting of our workflows and the activities that our implementations can use
	- MagicEightBall.Web: The web site interface for end-users to pick an EightBall and answer a question
         