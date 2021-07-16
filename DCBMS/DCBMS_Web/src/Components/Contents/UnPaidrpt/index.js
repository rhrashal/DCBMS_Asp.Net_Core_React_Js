import React, { useEffect, useState } from "react";
import { NavLink, useHistory } from "react-router-dom";
import { httpSimpleRequest } from "../../Utils/httpClient";
import { setCookie, getCookie, deleteCookie } from "../../Utils/cookies";
import { TextInput, InputForDate } from "../../Form";
import "react-datepicker/dist/react-datepicker.css";

let UnPaidReport = (props) => {
  let [Filter, setFilter] = useState({
    FromDate: "",
    ToDate: "",
  });

  let [allTest, setAllTest] = useState([]);

  let historyObj = useHistory();
  let routChange = (value) => {
    historyObj.push(value);
  };

  useEffect(() => {
    let loginToken = getCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY);
    if (!loginToken) {
      routChange(`/signin`);
      return;
      //console.log("Token Found", loginToken);
      //GetAllTestType();
    }
  }, []);

  let handleChange = ({ currentTarget: input }) => {
    ////console.log("input", input);

    let newFilter = { ...Filter, [input.name]: input.value };
    //console.log("newTest", newFilter);
    setFilter(newFilter);
  };

  const handleSubmit = (element) => {
    element.preventDefault();

    //console.log("signinObj", Filter);
    let token = getCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY);
    let httpRequest = {
      method: "post",
      url: `${process.env.REACT_APP_API_HOST_URL}/UnPaidBillReport`,
      data: Filter,
      headers: {
        "content-type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    };

    httpSimpleRequest(httpRequest)
      .then((response) => {
        //console.log("response", response.data);
        if (response?.data) {
          //setTest({ testTypeName: "" });
          //GetAllTestType();
          //alert(response?.data.results);
          setAllTest(response?.data.results);
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
        //console.log("error", error);
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
                    UnPaid Report
                  </div>
                </div>
                <form onSubmit={handleSubmit}>
                  <div className="row justify-content-center  mx-2">
                    <div className="col-5 col-lg-5 col-md-5 col-sm-5 errorStyle">
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
                        //minDate={new Date()}
                      />
                    </div>
                    <div className="col-5 col-lg-5 col-md-5 col-sm-5 errorStyle">
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
                    <div className="col-2 col-lg-2 col-md-2 col-sm-2 errorStyle">
                      <button
                        type="submit"
                        className="btn new_bnt_1 font-weight-bold mt-4"
                      >
                        Show
                      </button>
                    </div>
                  </div>

                  {/* <div className="row mx-2 justify-content-center">
                    <div className="col-2 "></div>
                  </div> */}
                </form>
                <div className="row justify-content-center border mt-5">
                  <table className="table mx-2 my-2 table-bordered">
                    <thead className="thead-light">
                      <tr>
                        <th scope="col">SL</th>
                        <th scope="col">Bill No</th>
                        <th scope="col">Contact</th>
                        <th scope="col">Patient Name</th>
                        <th scope="col">Bill Amount</th>
                      </tr>
                    </thead>
                    <tbody>
                      {allTest.map((item, index) => {
                        return (
                          <tr key={index}>
                            {/* <th scope="row">{item.id}</th> */}
                            <th scope="row">{index + 1}</th>
                            <td>{item.billNo}</td>
                            <td>{item.mobile}</td>
                            <td>{item.patientName}</td>
                            <td>{item.totalAmount}</td>
                          </tr>
                        );
                      })}
                      <tr>
                        <th colSpan="4">Total</th>
                        <th>{allTest[0]?.grandTotal}</th>
                      </tr>
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

export default UnPaidReport;
