import React from "react";
import { Route, Switch, useHistory, Redirect } from "react-router-dom";
import { Signin, Home, TestType ,TestSetup} from "../Contents";

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
        {/* <Route path="/authe/signup" exact render={() => <Signup />}></Route>
                <Route path="/admin/users" exact render={() => <Users />}></Route> */}
        <Route render={() => <div>Not Found</div>}></Route>
      </Switch>
    </div>
  );
}

export default HomeRoute;
