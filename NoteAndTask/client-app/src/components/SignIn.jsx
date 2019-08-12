import React from "react";
import { AuthLayout } from "./layout/AuthLayout";

export class SignIn extends React.Component {
  static displayName = SignIn.name;

  handleSubmit = async event => {
    event.preventDefault();

    if (event.target.checkValidity()) {
      const formData = {
        login: event.target.login.value,
        password: event.target.password.value,
        confirmPassword: event.target.confirmPassword.value
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
            this.props.history.push("/dashboard");
          }
        });
    } else {
      event.target.reportValidity();
    }
  };

  render() {
    return (
      <AuthLayout>
        <div className="col-md-5 text-center shadow bg-light" id="signInUpForm">
          <form id="signInForm" onSubmit={this.handleSubmit}>
            <h2 className="mb-4 mt-0 text-center">Sign In</h2>
            <div>
              <div className="form-group">
                <input
                  type="text"
                  className="form-control"
                  name="login"
                  placeholder="Your login"
                  required
                />
              </div>
              <div className="form-group">
                <input
                  type="text"
                  className="form-control"
                  name="password"
                  placeholder="Password"
                  required
                />
              </div>
              <div className="form-group">
                <input
                  type="text"
                  className="form-control"
                  name="confirmPassword"
                  placeholder="Confirm password"
                  required
                />
              </div>
            </div>
            <div>
              <input
                className="btn btn-primary"
                type="submit"
                value="Sign in"
              />
            </div>
          </form>
        </div>
      </AuthLayout>
    );
  }
}
