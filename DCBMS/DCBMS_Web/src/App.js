import React from 'react';
import { BrowserRouter  } from 'react-router-dom';
import Header from './Components/headerFooter/header';
import Footer from './Components/headerFooter/footer';
import HomeRouter from './Components/Routers/homeRouter';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
 import './Resources/css/style.css';

import ReactNotification from 'react-notifications-component';
import 'react-notifications-component/dist/theme.css'

function App() {

  return (
    <BrowserRouter>
      <main role="main" className="flex-shrink-0">
        {/* <ReactNotification /> */}
        <Header />
        <HomeRouter />
      </main>
      <Footer />
    </BrowserRouter>
  );
}

export default App;
