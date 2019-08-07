import React from 'react';
import {AuthLayout} from "./layout/AuthLayout";

export class SignUp extends React.Component {
    static displayName = SignUp.name;

    handleSubmit = async (event) => {
        event.preventDefault();

        if (event.target.checkValidity()) {

            let formData = {
                "Name": event.target.name.value,
                "Email" : event.target.email.value,
                "PhoneNumber" : event.target.phoneNumber.value,
                "Password": event.target.password.value,
                "PasswordCompare": event.target.passwordCompare.value
            };

            await fetch("api/auth/signup", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(formData)
            }).then(response => response.text())
                .then(data =>
                {
                    if(data) {
                        localStorage.setItem("token", data);
                        this.props.history.push("/board");
                    }
                })
        } else {
            event.target.reportValidity()
        }
    };

    render() {
        return (
            <AuthLayout>
                <div className="col-md-5 text-center shadow bg-light" id="signInUpForm">
                    <form id="signInForm" onSubmit={this.handleSubmit}>
                        <h2 className="mb-4 mt-0 text-center">Sign Up</h2>
                        <div>
                            <div className="form-group">
                                <input type="text" className="form-control" name="name" placeholder="Your name"
                                       required/>
                            </div>
                            <div className="form-group">
                                <input type="text" className="form-control" name="email" placeholder="Your email"
                                       required/>
                            </div>
                            <div className="form-group">
                                <input type="text" className="form-control" name="phoneNumber"
                                       placeholder="Your mobile number" required/>
                            </div>
                            <div className="form-group">
                                <input type="text" className="form-control" name="password" placeholder="Password"
                                       required/>
                            </div>
                            <div className="form-group">
                                <input type="text" className="form-control" name="passwordCompare"
                                       placeholder="Repeat your password" required/>
                            </div>
                        </div>
                        <div>
                            <input className="btn btn-primary" type="submit" value="Sign up"/>
                        </div>
                    </form>
                </div>
            </AuthLayout>
        );
    }
}