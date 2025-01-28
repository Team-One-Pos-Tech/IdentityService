Feature: Sign Up and Sign In Use Case

    Scenario: New client sign up, should be able to sign in
        Given a SignUpRequest with name 'John Doe', cpf '98430490043', password 'JohnSup3rP4ss' and email 'john-doe@mail.com'
        When persisting it into the database
        Then the client with cpf '98430490043' should be stored
        Then an event of type 'ClientCreated' is raised
        Then it should be able to sign in by using cpf '98430490043' and password 'JohnSup3rP4ss'