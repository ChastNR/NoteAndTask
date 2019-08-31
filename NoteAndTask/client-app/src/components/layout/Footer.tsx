import * as React from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";

const FooterElement = styled.footer`
  width: 90vw;
  margin-left: auto;
  margin-right: auto;
`

const FooterUl = styled.ul`
  list-style-type: none;
`

export const Footer: React.FC = () => {
  return (
    <FooterElement>
      <div>
        <nav>
          <div>
            2019&nbsp;©&nbsp;<Link to="/">Note & Task</Link>
          </div>
          <div>
            <FooterUl>
              <li>
                <Link to="/">About</Link>
              </li>
              <li>
                <Link to="/">Team</Link>
              </li>
              <li>
                <Link to="/">Privacy</Link>
              </li>
            </FooterUl>
          </div>
        </nav>
      </div>
    </FooterElement>
  );
};
