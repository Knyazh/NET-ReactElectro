import React from "react";
import "./footer.css";
import { SlEnvolopeLetter } from "react-icons/sl";

import {
  MDBFooter,
  MDBContainer,
  MDBRow,
  MDBCol,
  MDBIcon,
} from "mdb-react-ui-kit";
import { Link } from "react-router-dom";

const Footer = () => {
  return (
    <>
      <div className="footer-newsletter">
        <div className="container d-flex justify-content-between">
          <div className="d-flex newsletter-title ">
          <SlEnvolopeLetter className="newsletter-icon" />
            <h5> Sign Up to Newsletter</h5>
            <span>...and receive <strong>$20 coupon for first shopping</strong> </span>
          </div>
          <div className="newsletter-input">
            <input type="text" placeholder="Enter your email address" ></input>
            <button>Sign Up</button>
          </div>
        </div>
      </div>
      <MDBFooter
        bgColor="light"
        className="text-center text-lg-start text-muted footer"
      >
        <section className="d-flex justify-content-center justify-content-lg-between p-4 border-bottom">
          <div className="me-5 d-none d-lg-block">
            <span>Get connected with us on social networks:</span>
          </div>

          <div>
            <Link  className="me-4 text-reset">
              <MDBIcon fab icon="facebook-f" />
            </Link>
            <Link  className="me-4 text-reset">
              <MDBIcon fab icon="twitter" />
            </Link>
            <Link  className="me-4 text-reset">
              <MDBIcon fab icon="google" />
            </Link>
            <Link  className="me-4 text-reset">
              <MDBIcon fab icon="instagram" />
            </Link>
            <Link  className="me-4 text-reset">
              <MDBIcon fab icon="linkedin" />
            </Link>
            <Link className="me-4 text-reset">
              <MDBIcon fab icon="github" />
            </Link>
          </div>
        </section>

        <section className="">
          <MDBContainer className="text-center text-md-start mt-5">
            <MDBRow className="mt-3">
              <MDBCol md="3" lg="4" xl="3" className="mx-auto mb-4">
                <h6 className="text-uppercase fw-bold mb-4">
                  <MDBIcon icon="gem" className="me-3" />
                  .Netreacktelectro
                </h6>
                <p>
                  Lorem ipsum dolor, sit amet consectetur adipisicing elit.
                  Consequatur rem libero esse. Error provident placeat quisquam,
                  omnis blanditiis quasi doloribus id deleniti laborum in
                  mollitia perspiciatis quia non tenetur dolorem!
                </p>
              </MDBCol>

              <MDBCol md="2" lg="2" xl="2" className="mx-auto mb-4">
                <h6 className="text-uppercase fw-bold mb-4">Products</h6>
                <p>
                  <a href="#!" className="text-reset">
                    C#
                  </a>
                </p>
                <p>
                  <a href="#!" className="text-reset">
                    React
                  </a>
                </p>
                <p>
                  <a href="#!" className="text-reset">
                    Asp.NET core Web API
                  </a>
                </p>
                <p>
                  <a href="#!" className="text-reset">
                    Authentication
                  </a>
                </p>
              </MDBCol>

              <MDBCol md="3" lg="2" xl="2" className="mx-auto mb-4">
                <h6 className="text-uppercase fw-bold mb-4">Useful links</h6>
                <p>
                  <a href="#!" className="text-reset">
                    Pricing
                  </a>
                </p>
                <p>
                  <a href="#!" className="text-reset">
                    Settings
                  </a>
                </p>
                <p>
                  <a href="#!" className="text-reset">
                    Orders
                  </a>
                </p>
                <p>
                  <a href="#!" className="text-reset">
                    Help
                  </a>
                </p>
              </MDBCol>

              <MDBCol md="4" lg="3" xl="3" className="mx-auto mb-md-0 mb-4">
                <h6 className="text-uppercase fw-bold mb-4">Contact</h6>
                <p>
                  <MDBIcon icon="home" className="me-2" />
                  Baki Seheri Code Academy
                </p>
                <p>
                  <MDBIcon icon="envelope" className="me-3" />
                  gnyazah@code.edu.az
                </p>
                <p>
                  <MDBIcon icon="phone" className="me-3" /> +994 50 777 33 22
                </p>
                <p>
                  <MDBIcon icon="print" className="me-3" /> +994 50 123 45 67
                </p>
              </MDBCol>
            </MDBRow>
          </MDBContainer>
        </section>

        <div
          className="text-center p-4"
          style={{ backgroundColor: "rgba(0, 0, 0, 0.05)" }}
        >
          © 2024 Copyright:
          <a className="text-reset fw-bold" href="https://mdbootstrap.com/">
            Net-reactElectro.com
          </a>
        </div>
      </MDBFooter>
    </>
  );
};

export default Footer;
