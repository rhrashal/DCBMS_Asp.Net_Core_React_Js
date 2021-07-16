import React from "react";
import { Route, Switch, useHistory, Redirect } from "react-router-dom";
import {  Signin,  Home,  TestType,  TestSetup,  TestWiseReport,  TypeWiseReport,  UnPaidReport,  TestRequest,PayBill} from "../Contents";

function HomeRoute() {
  const history = useHistory();
  let isLogin = true;
  return (
    <div className="container">
      <Switch>
        <Redirect from="/" to="/signin" exact />
        <Route path="/signin" exact render={() => <Signin />}></Route>
        <Route path="/home" exact render={() => <Home />}></Route>
        <Route path="/test-type" exact render={() => <TestType />}></Route>
        <Route path="/test" exact render={() => <TestSetup />}></Route>
        <Route path="/entry" exact render={() => <TestRequest />}></Route>
        <Route path="/payment" exact render={() => <PayBill />}></Route>
        <Route path="/test-wise"  exact  render={() => <TestWiseReport />}></Route>
        <Route path="/type-wise" exact render={() => <TypeWiseReport />}></Route>
        <Route path="/unpaid-bill" exact render={() => <UnPaidReport />}></Route>      
        <Route render={() => <div style={{color: "red",textAlign:"center"}}>Not Found</div>}></Route>
      </Switch>
    </div>
  );
}

export default HomeRoute;
