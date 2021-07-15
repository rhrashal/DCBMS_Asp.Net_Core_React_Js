import React, { useEffect, useState } from "react";
import { NavLink, useHistory } from "react-router-dom";
import { httpSimpleRequest } from "../../Utils/httpClient";
import { setCookie, getCookie, deleteCookie } from "../../Utils/cookies";
import { formFieldName } from "./formField";
import { TextInput, InputForDate } from "../../Form";
import "react-datepicker/dist/react-datepicker.css";

let TestWiseReport = (props) => {
  let [Filter, setFilter] = useState({
    FromDate: "",
    ToDate: "",
  });

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
      // routChange(`/test-type`);
      console.log("Token Found", loginToken);
      //GetAllTestType();
    }
  }, []);

  let handleChange = ({ currentTarget: input }) => {
    console.log("input", input);

    let newFilter = { ...Filter, [input.name]: input.value };
    console.log("newTest", newFilter);
    setFilter(newFilter);
  };

  const handleSubmit = (element) => {
    element.preventDefault();

    console.log("signinObj", Filter);
    let token = getCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY);
    let httpRequest = {
      method: "post",
      url: `${process.env.REACT_APP_API_HOST_URL}/TestWiseReport`,
      data: Filter,
      headers: {
        "content-type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    };

    httpSimpleRequest(httpRequest)
      .then((response) => {
        console.log("response", response.data);
        if (response?.data) {
          //setTest({ testTypeName: "" });
          //GetAllTestType();
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
                    <div className="col-6 col-lg-6 col-md-6 col-sm-6 errorStyle">
                      <label htmlFor="FromDate">FromDate </label>
                      <InputForDate
                        onChange={(dateValue) =>
                          handleChange({
                            currentTarget: {
                              name: "FromDate",
                              value: dateValue,
                            },
                          })
                        }
                        name="FromDate"
                        type="text"
                        autoFocus=""
                        errors=""
                        value={Filter.FromDate ? new Date(Filter.FromDate) : ""}
                        minDate={new Date()}
                      />
                    </div>
                    <div className="col-6 col-lg-6 col-md-6 col-sm-6 errorStyle">
                      <label htmlFor="ToDate">ToDate </label>
                      <InputForDate
                        onChange={(dateValue) =>
                          handleChange({
                            currentTarget: {
                              name: "ToDate",
                              value: dateValue,
                            },
                          })
                        }
                        name="ToDate"
                        type="text"
                        autoFocus=""
                        errors=""
                        value={Filter.ToDate ? new Date(Filter.ToDate) : ""}
                        minDate={new Date(Filter.FromDate)}
                      />
                    </div>
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
                      </tr>
                    </thead>
                    <tbody>
                      {allTest.map((item, index) => {
                        return (
                          <tr key={index}>
                            {/* <th scope="row">{item.id}</th> */}
                            <th scope="row">{index + 1}</th>
                            <td>{item.testTypeName}</td>
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

export default TestWiseReport;
