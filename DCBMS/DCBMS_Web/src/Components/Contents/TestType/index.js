import React, { useEffect, useState } from "react";
import { NavLink, useHistory } from "react-router-dom";
import { httpSimpleRequest } from "../../Utils/httpClient";
import { setCookie, getCookie, deleteCookie } from "../../Utils/cookies";
import { formFieldName } from "./formField";
import { TextInput } from "../../Form";

let TestType = (props) => {
  let [test, setTest] = useState({ testTypeName: "" });

  let [allTest, setAllTest] = useState([
    { id: 1, testTypeName: "1st Type" },
    { id: 2, testTypeName: "2st Type" },
  ]);

  let historyObj = useHistory();
  let routChange = (value) => {
    historyObj.push(value);
  };

  useEffect(() => {
    let loginToken = getCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY);
    if (loginToken) {
       routChange(`/test-type`);
      console.log("Token Found", loginToken);
       GetAllTestType();
    }
  }, []);

  let handleChange = ({ currentTarget: input }) => {
    let newTest = { ...test, [input.name]: input.value };
    console.log("newTest", newTest);
    setTest(newTest);
  };

  const handleSubmit = (element) => {
    element.preventDefault();
    let signinSubmitArr = element.target;
    let signinObj = {};

    for (let i = 0; i < signinSubmitArr.length; i++) {
      const value = signinSubmitArr[i].value;
      const name = signinSubmitArr[i].name;
      if (name !== "") {
        signinObj[name] = value;
      }
    }

    console.log("signinObj", signinObj);
    let token = getCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY);
    let httpRequest = {
      method: "post",
      url: `${process.env.REACT_APP_API_HOST_URL}/AddTestType`,
      data: signinObj,
      headers: {
        "content-type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    };

    httpSimpleRequest(httpRequest)
      .then((response) => {
        console.log("response", response.data);
        if (response?.data) {
          setTest({ testTypeName: "" });
           GetAllTestType();
          alert(response?.data.results);
          //setAllTest(response.data);
        } else {
          let notifyOptions = {
            title: "Error",
            message: response.data.error || "Incorrect username or password.",
            type: "danger",
          };
          // notifications(notifyOptions);
        }
      })
      .catch((error) => {
        console.log("error", error);
        let notifyOptions = {
          title: "Error",
          message: "Incorrect username or password.",
          type: "danger",
        };
        // notifications(notifyOptions);
      });
  };

  const GetAllTestType = () => {
    let token = getCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY);
    let httpRequest = {
      method: "get",
      url: `${process.env.REACT_APP_API_HOST_URL}/GetTestTypeList`,
      headers: {
        "content-type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    };

    httpSimpleRequest(httpRequest)
      .then((response) => {
        console.log("response", response.data);
        if (response?.data) {
          setAllTest(response.data.results);
        } else {
          let notifyOptions = {
            title: "Error",
            message: response.data.error || "Incorrect username or password.",
            type: "danger",
          };
          // notifications(notifyOptions);
        }
      })
      .catch((error) => {
        console.log("error", error);
        let notifyOptions = {
          title: "Error",
          message: "Incorrect username or password.",
          type: "danger",
        };
        // notifications(notifyOptions);
      });
  };

  const DeleteTestType = (id) => {
    console.log("Id", id);
    if(id<1){
      return;
    }
   if(!window.confirm('Delete the item?')){
     return;
   }
    let token = getCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY);
    let httpRequest = {
      method: "delete",
      url: `${process.env.REACT_APP_API_HOST_URL}/DeleteTestType?testTypeId=`+ id,
      headers: {
        "content-type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    };

    httpSimpleRequest(httpRequest)
      .then((response) => {
        console.log("response", response.data);
        if (response?.data) {
           GetAllTestType();
           alert(response?.data.results);
        } else {
          let notifyOptions = {
            title: "Error",
            message: response.data.error || "Incorrect username or password.",
            type: "danger",
          };
          // notifications(notifyOptions);
        }
      })
      .catch((error) => {
        console.log("error", error);
        let notifyOptions = {
          title: "Error",
          message: "Incorrect username or password.",
          type: "danger",
        };
        // notifications(notifyOptions);
      });
  };
  return (
    <div className="row justify-content-center mt-5">
      <div className="col-12 col-md-12 col-xl-10 col-lg-10 col-sm-12">
        <div className="container custom_form mt-5">
          <div className="row mt-0 mr-n4 ml-n4">
            <div className="col-12">
              <div className="container">
                <div className="row justify-content-center  font-weight-bold h3 ">
                  <div className="col-12 mx-0  border-top-0 border-right-0 border-left-0  border-bottom text-center pb-2">
                    Test Type Setup
                  </div>
                </div>
                <form onSubmit={handleSubmit}>
                  <div className="row justify-content-center  mx-2">
                    {formFieldName.map((item, itemIndex) => {
                      return (
                        <TextInput
                          key={itemIndex}
                          {...item}
                          value={test.testTypeName}
                          error=""
                          onChange={handleChange}
                        />
                      );
                    })}
                  </div>
                  <div className="row mx-2 justify-content-center">
                    <div className="col-2 ">
                      <button
                        type="submit"
                        className="btn new_bnt_1 font-weight-bold "
                      >
                        Save
                      </button>
                    </div>
                  </div>
                </form>
                <div className="row justify-content-center border mt-5">
                  <table className="table mx-2 my-2 table-bordered">
                    <thead className="thead-light">
                      <tr>
                        <th scope="col">SL</th>
                        <th scope="col">Type Name</th>
                        <th scope="col">Action</th>
                      </tr>
                    </thead>
                    <tbody>
                      {allTest.map((item, index) => {
                        return (
                          <tr key={index}>
                            {/* <th scope="row">{item.id}</th> */}
                            <th scope="row">{index+1}</th>
                            <td>{item.testTypeName}</td>
                            <td><button type="button" className="btn btn-danger" onClick={()=>DeleteTestType(item.id)} > X</button></td>
                          </tr>
                        );
                      })}
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default TestType;
