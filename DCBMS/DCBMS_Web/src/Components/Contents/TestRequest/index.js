import React, { useEffect, useState } from "react";
import { NavLink, useHistory } from "react-router-dom";
import { httpSimpleRequest } from "../../Utils/httpClient";
import { setCookie, getCookie, deleteCookie } from "../../Utils/cookies";
import { TextInput, InputForDate } from "../../Form";
import "react-datepicker/dist/react-datepicker.css";
import { ready } from "jquery";

let TestRequest = (props) => {
  let [request, setRequest] = useState({
    patientName: "",
    dateOfBirth: "",
    mobile: "",
    totalAmount: 0,
    testId: 0,
    testName: "",
    payableAmount: 0,
    testRequestList: [
      // {
      //   testId: 0,
      //   testName: "",
      //   payableAmount: 0,
      // },
    ],
  });

  let [responseRequest, setResponseRequest] = useState({
    id: 0,
    patientName: "",
    dateOfBirth: "",
    mobile: "",
    billNo: "",
    testDate: "",
    dueDate: "",
    paymentDate: "",
    totalAmount: 0,
    status: "",
    isPaid: false,
    isComplete: false,
    isDelivered: false,
    testRequestList: [
      {
        id: 0,
        patientId: 0,
        testId: 0,
        payableAmount: 0,
        testName: "",
      },
    ],
  });

  let [test, setTest] = useState({
    id: 1,
    testName: "",
    fee: 0.0,
    testTypeId: 1,
  });

  let [allTest, setAllTest] = useState([
    { id: 0, testName: "", fee: 0.0, testTypeId: 0, testTypeName: "" },
  ]);

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
    }
    GetAllTests();
  }, []);

  const GetAllTests = () => {
    let token = getCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY);
    let httpRequest = {
      method: "get",
      url: `${process.env.REACT_APP_API_HOST_URL}/GetTestList`,
      headers: {
        "content-type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    };

    httpSimpleRequest(httpRequest)
      .then((response) => {
        //console.log("response", response.data);
        if (response?.data) {
          setAllTest(response.data.results);
          //console.log(response.data.results);
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

  let handleChange = ({ currentTarget: input }) => {
    ////console.log("input", input.name, input.value);
    let newTest = { ...request, [input.name]: input.value };

    //console.log("newTest", newTest);
    setRequest(newTest);
    if (input.name == "testId") {
      allTest.filter((item, newIndex) => {
        if (item.id == input.value) {
          ////console.log(item);
          let newReq = { ...newTest, payableAmount: item.fee };
          // //console.log("newReq", newReq);
          setRequest(newReq);
          //request.payableAmount = item.fee;
          //setRequest(request);
        }
      });
    }
  };

  const addTest = (element) => {
    element.preventDefault();
    //console.log(request.testId);
    allTest.filter((item, newIndex) => {
      if (item.id == request.testId) {
        request.testRequestList.push({
          testId: item.id,
          testName: item.testName,
          payableAmount: request.payableAmount,
        });
        //testType = typeItem.testType;
      }
    });
    let newTest = { ...request, testName: "" };
    setRequest(newTest);
  };

  const handleSubmit = (element) => {
    element.preventDefault();

    //console.log("request", request);
    let token = getCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY);
    let httpRequest = {
      method: "post",
      url: `${process.env.REACT_APP_API_HOST_URL}/AddPatientRequest`,
      data: request,
      headers: {
        "content-type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    };

    httpSimpleRequest(httpRequest)
      .then((response) => {
        //console.log("response", response.data);
        if (response?.data) {
          setResponseRequest(response.data.results);
        }
      })
      .catch((error) => {
        //console.log("error", error);
        let notifyOptions = {
          title: "Error",
          message: "Incorrect username or password.",
          type: "danger",
        };
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
                    Test Request Entry
                  </div>
                </div>
                <form onSubmit={addTest}>
                  <div className="row justify-content-center  mx-2">
                    <div className="form-group col-md-6">
                      <label className="">Patient Name</label>
                      <input
                        type="text"
                        className="form-control"
                        name="patientName"
                        id="patientName"
                        value={request.patientName}
                        onChange={handleChange}
                      ></input>
                    </div>

                    <div className="form-group col-md-6">
                      <label>Date Of Birth </label>
                      <InputForDate
                        onChange={(dateValue) =>
                          handleChange({
                            currentTarget: {
                              name: "dateOfBirth",
                              value: dateValue,
                            },
                          })
                        }
                        name="dateOfBirth"
                        type="text"
                        autoFocus=""
                        errors=""
                        value={
                          request.dateOfBirth
                            ? new Date(request.dateOfBirth)
                            : ""
                        }
                        //minDate={new Date(Filter.FromDate)}
                      />
                    </div>

                    <div className="form-group col-md-6">
                      <label className="">Mobile No</label>
                      <input
                        type="text"
                        className="form-control"
                        name="mobile"
                        id="mobile"
                        value={request.mobile}
                        onChange={handleChange}
                      ></input>
                    </div>

                    <div className="form-group col-md-6 ">
                      <label>Test </label>
                      <select
                        value={request.testId}
                        //defaultValue="0"
                        className="form-control mb-2 "
                        name="testId"
                        onChange={handleChange}
                      >
                        <option>Choose Test</option>
                        {allTest.map((item, index) => {
                          return (
                            <option key={index} value={item.id}>
                              {item.testName}
                            </option>
                          );
                        })}
                      </select>
                    </div>

                    <div className="form-group col-md-6">
                      <label className="">Fee</label>
                      <input
                        type="text"
                        className="form-control"
                        name="payableAmount"
                        id="payableAmount"
                        value={request.payableAmount}
                        onChange={handleChange}
                      ></input>
                    </div>

                    <div className="form-group col-md-6">
                      <div className="float-right">
                        <button
                          type="submit"
                          className="btn new_bnt_1 font-weight-bold mt-4"
                        >
                          Add
                        </button>
                      </div>
                    </div>
                  </div>
                  {/* <div className="row  mx-2"></div> */}
                </form>
                { request.testRequestList.length>0 && 
                <div className="row justify-content-center border mt-5">
                  
                  <table className="table mx-2 my-2 table-bordered">
                    <thead className="thead-light">
                      <tr>
                        <th scope="col">SL</th>
                        <th scope="col">Test Name</th>
                        <th scope="col">Fee</th>
                      </tr>
                    </thead>
                    <tbody>
                      {request.testRequestList.map((item, index) => {
                        if (item.testName == "") {
                          return;
                        } else {
                          return (
                            <tr key={index}>
                              {/* <td scope="row">{item.id}</td> */}
                              <th scope="row">{index+1}</th>
                              <td scope="row">{item.testName}</td>
                              <td scope="row">{item.payableAmount}</td>
                            </tr>
                          );
                        }
                      })}
                    </tbody>
                  </table>
                  
                    <div className="form-group col-md-12">
                    <div className="float-right">
                      <button  type="button" className="btn new_bnt_1 font-weight-bold mt-4" onClick={handleSubmit} >Save</button>
                    </div>
                  </div>
                  
               
                  {/* <div className="form-group col-md-12">
                    <div className="float-right">
                      <button  type="button" className="btn new_bnt_1 font-weight-bold mt-4" onClick={handleSubmit} >Save</button>
                    </div>
                  </div> */}
                </div>
}
              </div>
            </div>
          </div>
        </div>
      </div>
{responseRequest.id>0 && 

<div className="col-12 col-md-12 col-xl-10 col-lg-10 col-sm-12">
<div className="container custom_form mt-5">
  <div className="row mt-0 mr-n4 ml-n4">
    <div className="col-12">
      <div className="container">
        <div className="row justify-content-center  font-weight-bold h3 ">
          <div className="col-12 mx-0  border-top-0 border-right-0 border-left-0  border-bottom text-center pb-2">
            Patient Details  <span className="badge badge-warning">{responseRequest.status}</span>
          </div>
        </div>
       
          <div className="row justify-content-center  mx-2">
            <div className="form-group col-md-6">
              <label className="">Patient Name :</label>
              <label className="ml-2"><b>{responseRequest.patientName}</b></label>                      
            </div>

            <div className="form-group col-md-6">
              <label>Date Of Birth :</label>
              <label  className="ml-2"><b>{responseRequest.dateOfBirth}</b></label>                      
            </div>

            <div className="form-group col-md-6">
              <label className="">Mobile No :</label>
              <label  className="ml-2"><b>{responseRequest.mobile}</b></label>
           </div>

           <div className="form-group col-md-6">
              <label className="">Bill No :</label>
              <label  className="ml-2"><b>{responseRequest.billNo}</b></label>
           </div>
           <div className="form-group col-md-6">
              <label className="">Test Date :</label>
              <label  className="ml-2"><b>{responseRequest.testDate}</b></label>
           </div>
           <div className="form-group col-md-6">
              <label className="">Total Amount :</label>
              <label  className="ml-2"><b>{responseRequest.totalAmount}</b></label>
           </div>
                     
         </div>

        <div className="row justify-content-center border mt-5">
          <table className="table mx-2 my-2 table-bordered">
            <thead className="thead-light">
              <tr>
                <th scope="col">SL</th>
                <th scope="col">Test Name</th>
                <th scope="col">Fee</th>
              </tr>
            </thead>
            <tbody>
              {responseRequest.testRequestList.map((item, index) => {
                if (item.testName == "") {
                  return;
                } else {
                  return (
                    <tr key={index}>
                      {/* <td scope="row">{item.id}</td> */}
                      <th scope="row">{index+1}</th>
                      <td scope="row">{item.testName}</td>
                      <td scope="row">{item.payableAmount}</td>
                    </tr>
                  );
                }
              })}
            </tbody>
          </table>                
        </div>
      </div>
    </div>
  </div>
</div>
</div>


}
     
    </div>
  );
};

export default TestRequest;
