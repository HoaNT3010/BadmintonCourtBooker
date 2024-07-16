import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
import "./App.css";
import Navigation from "./common/Navigation";
import { Container, Content, Footer, Header } from "rsuite";
import { Routes } from "react-router-dom";


function App() {
  return (
    <>
      <Container>
        <Header>
          <Navigation />
        </Header>
        <Content>
          <div className="custom-body">
            <Routes>
              {/* <Route path="/" element={<HomePage />}></Route>
              <Route path="/detail/:id" element={<Detail />}></Route>
              <Route path="/admin/" element={<AdminPage />}></Route>
              <Route path="/admin/add/" element={<CreatePage />}></Route>
              <Route path="/admin/udpate/:id" element={<UpdatePage />}></Route>
              <Route path="/about" element={<AboutPage />} />
              <Route path="/contact" element={<ContactPage />} /> */}
            </Routes>
          </div>
        </Content>
        <Footer>Footer</Footer>
      </Container>

      {/* <Footer/> */}
    </>
  );
}

export default App;
