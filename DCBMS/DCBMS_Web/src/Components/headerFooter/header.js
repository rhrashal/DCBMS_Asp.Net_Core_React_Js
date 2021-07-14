import React from 'react';
import { FaSignOutAlt,FaSearch } from "react-icons/fa";
import { NavLink, useHistory } from 'react-router-dom';
import { setCookie, getCookie, deleteCookie } from '../Utils/cookies';

const Header = () =>{
    let historyObj = useHistory();
    let routChange = (value) => {
        historyObj.push(value)
    };
    const logout = ()=>{
        deleteCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY);
        routChange(`/`);
    }

    return (
        <div className="container-fluid">
            <div className="row navbar navbar-expand-lg navbar-light bg-light p-2 shadow bg-light rounded">
                <div className="col-auto mr-auto float-left">
                    <div>
                        <NavLink className="navbar-brand" to="/">Home</NavLink>
                    </div>
                </div>
                <div class="dropdown">
                    <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        Setup
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <NavLink className="dropdown-item" to="/test-type">Test Type</NavLink>
                    <NavLink className="dropdown-item" to="/test">Test</NavLink>
                    </div>
                </div>

                <div class="dropdown mx-2">
                    <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        Test Request
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <NavLink className="dropdown-item" to="/entry">Entry</NavLink>
                    <NavLink className="dropdown-item" to="/payment">Payment</NavLink>
                    </div>
                </div>

                <div class="dropdown">
                    <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        Report
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <NavLink className="dropdown-item" to="/test-wise">Test Wise</NavLink>
                    <NavLink className="dropdown-item" to="/type-wise">Type Wise</NavLink>
                    <NavLink className="dropdown-item" to="/unpaid-bill">Unpaid Bill</NavLink>
                    </div>
                </div>
                <div className="col-auto float-right">
                    <button onClick={logout} className="btn btn-outline-primary btn-sm" to="/signout"><span className="mr-2">Sign Out</span><span><FaSignOutAlt /></span></button>
                </div>
            </div>
        </div>
    );
}

export default Header;
