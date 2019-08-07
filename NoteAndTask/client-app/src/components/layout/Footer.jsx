import React from 'react';
import {Container} from 'react-bootstrap';
import "./Footer.css";

export class Footer extends React.Component {
    static displayName = Footer.name;

    render() {
        return (
            <footer>
                <div>
                    <nav className="navbar">
                        <div className="align-items-center">
                            2019&nbsp;©&nbsp;<a href="">Note & Task</a>
                        </div>
                        <div>
                            <ul className="footer-links">
                                <li>
                                    <a href="#">About</a>
                                </li>
                                <li><a href="#">Team</a></li>
                                <li><a href="#">Privacy</a></li>
                            </ul>
                        </div>
                    </nav>
                </div>
            </footer>
        );
    }
}

// return (
//     <footer>
//         <Container>
//             <div className="text-center">
//                 <span>Copyright &copy; @DateTime.Now.Year <a href="/"> NoteAndTask </a>All rights reserved. All trademarks used are properties of their respective owners.</span>
//             </div>
//         </Container>
//     </footer>
// );