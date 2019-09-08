import * as React from "react";
import { AuthLayout } from "../layout/AuthLayout";
import { Button, ButtonToolbar, Content, ControlLabel, FlexboxGrid, Form, FormGroup, Panel } from "rsuite";

interface ILoginModel {
  login: string;
  password: string;
}

export const SignIn: React.FC = () => {
  
  const handleSubmit = async (event: any) => {
    event.preventDefault();

    if (event.target.checkValidity()) {
      const formData: ILoginModel = {
        login: event.target.login.value,
        password: event.target.password.value
      };

      await fetch("api/auth/signin", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(formData)
      })
          .then(response => response.text())
          .then(data => {
            if (data) {
              localStorage.setItem("token", data);
              window.location.href = "/dashboard";
              //props.history.push("/dashboard");
            }
          });
    } else {
      event.target.reportValidity();
    }
  };
  
    return (
      <AuthLayout>
        <Content>
          <FlexboxGrid justify="center">
            <FlexboxGrid.Item colspan={12}>
              <Panel className="shadow-box" header={<h3>Sign In</h3>} bordered>
                <Form fluid id="signInForm" onSubmit={handleSubmit}>
                  <FormGroup>
                    <ControlLabel>Email or phone number</ControlLabel>
                    <input className="rs-input" type="text" name="login" required />
                  </FormGroup>
                  <FormGroup>
                    <ControlLabel>Password</ControlLabel>
                    <input className="rs-input" type="text" name="password" required />
                  </FormGroup>
                  <FormGroup>
                    <ButtonToolbar>
                      <Button type="submit" appearance="primary">
                        Sign in
                      </Button>
                      <Button appearance="link">Forgot password?</Button>
                    </ButtonToolbar>
                  </FormGroup>
                </Form>
              </Panel>
            </FlexboxGrid.Item>
          </FlexboxGrid>
        </Content>
      </AuthLayout>
    );
};
