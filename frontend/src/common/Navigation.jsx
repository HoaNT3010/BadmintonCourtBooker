import React, { Component } from "react";
import { Button } from "react-bootstrap";
import Nav from 'react-bootstrap/Nav';
import ReactDOM from "react-dom";
import { Link } from "react-router-dom";
import { Navbar } from "rsuite";
import { toggleIsAuthenticated } from "../features/Auth/data/Slice";
import { useDispatch, useSelector } from "react-redux";
import axios from "axios";

export default function Navigation() {
  const isLogin = useSelector((state) => state.auth);
  const dispatch = useDispatch();

  const onLoginSuccess = async (tokenResponse) => {
    // const decoded = jwtDecode();
    const userInfo = await axios
      .get("https://www.googleapis.com/oauth2/v3/userinfo", {
        headers: { Authorization: `Bearer ${tokenResponse.access_token}` },
      })
      .then((response) => dispatch(toggleIsAuthenticated(response)));
  };

  const onLogoutSuccess = () => {
    console.log("logout");
    dispatch(toggleIsAuthenticated({}));
  };

  const login = useGoogleLogin({
    onSuccess: (tokenResponse) => onLoginSuccess(tokenResponse),
    onError: () => console.log("login"),
  });

  return (
    <Navbar className="custom-nav-bar" variant="underline">
      <Link className="navbar-logo" to={`/`}>
        <img src="/FlowerLogo.jpeg"></img>
      </Link>
      <div className="nav-item-bar">
        <Nav.Item>
          <Link to={`/`}>
          <Button variant="success">Flower</Button>
          </Link>
        </Nav.Item>
        {isLogin.isAuthenticated ? (
          <Nav.Item>
            <Link to={`/admin`} >
            <Button variant="success">Admin</Button>
            </Link>
          </Nav.Item>
        ) : null}
        <Nav.Item>
          <Link to={`/about`} >
          <Button variant="success">About</Button>
          </Link>
        </Nav.Item>
        <Nav.Item>
          <Link to={`/contact`} >
          <Button variant="success">Contact</Button>
          </Link>
        </Nav.Item>
      </div>
      <div className="auth-button">
        {isLogin.isAuthenticated ? (
          <div>
            <p> Welcome, {isLogin.data.name} </p>
            &nbsp;&nbsp;
            <button onClick={onLogoutSuccess}>Logout</button>
          </div>
        ) : (
          <button onClick={() => login()}>Login</button>
        )}
      </div>
    </Navbar>
  );
}
