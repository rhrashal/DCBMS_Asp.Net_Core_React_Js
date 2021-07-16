import React, { useEffect, useState } from "react";
import { NavLink, useHistory } from "react-router-dom";
import { httpSimpleRequest } from "../../Utils/httpClient";
import { setCookie, getCookie, deleteCookie } from "../../Utils/cookies";
import { formFieldName } from "./formField";
import { TextInput } from "../../Form";

let PayBill = (props) => {
  let [billInfo, setBillInfo] = useState({
    id: 11,
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
    grandTotal: 0
});


let [Filter, setFilter] = useState({
  mobile: "",
  billNo: ""
});


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
    let newTest = { ...Filter, [input.name]: input.value };
    console.log("newTest", newTest);
    setFilter(newTest);
  };

  const handleSubmit = (element) => {
    element.preventDefault();
   console.log("recall");
    console.log("Filter", Filter);
    let token = getCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY);
    let httpRequest = {
      method: "post",
      url: `${process.env.REACT_APP_API_HOST_URL}/ScerchBill`,
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
          if(response?.data.results != null){
            setBillInfo(response?.data.results);
          }
          
          //  GetAllTestType();
         // alert(response?.data.message);
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

  const billPayment = (element) => {
    element.preventDefault();
    if(billInfo.id<1){
      alert("Bill Id Requared.");
      return;
    }
    if(!window.confirm('Do You Want to Pay?')){
      return;
    }
    console.log("Filter", Filter);
    let token = getCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY);
    let httpRequest = {
      method: "get",
      url: `${process.env.REACT_APP_API_HOST_URL}/BillPay?Id=`+ billInfo.id,
      headers: {
        "content-type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    };

    httpSimpleRequest(httpRequest)
      .then((response) => {
        console.log("response", response.data);
        if (response?.data) {
          //setBillInfo(response?.data.results);
          handleSubmit();
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
                    {formFieldName.map((item, itemIndex) => {
                      return (
                        <TextInput
                          key={itemIndex}
                          {...item}
                          value={item.value}
                          error=""
                          onChange={handleChange}
                        />
                      );
                    })}
                  </div>
                  <div className="row mx-2 justify-content-center">
                    <div className="col-12">
                      <div className="float-right">
                      <button type="submit" className="btn new_bnt_1 font-weight-bold "> Search  </button>
                      </div>                      
                    </div>
                  </div>
                </form>
                <div className="row justify-content-center border mt-5">
                 </div>
              </div>
            </div>         
          </div>
        </div>
      </div>
      <div className="col-12 col-md-12 col-xl-10 col-lg-10 col-sm-12">
        <div className="container custom_form mt-5">
          <div className="row mt-0 mr-n4 ml-n4">
            <div className="col-12">
              <div className="container">
                <div className="row justify-content-center  font-weight-bold h3 ">
                  <div className="col-12 mx-0  border-top-0 border-right-0 border-left-0  border-bottom text-center pb-2">
                    Payment Details
                  </div>
                </div>
               
                  <div className="row justify-content-center  mx-2">
                    <div className="form-group col-md-6">
                      <label className="">Patient Name :</label>
                      <label className="ml-2"><b>{billInfo.patientName}</b></label>                      
                    </div>

                    <div className="form-group col-md-6">
                      <label>Status :</label>
                      <label  className="ml-2"><b><span className="badge badge-warning">{billInfo.status}</span></b></label>                      
                    </div>

                    <div className="form-group col-md-6">
                      <label>Date Of Birth :</label>
                      <label  className="ml-2"><b>{billInfo.dateOfBirth}</b></label>                      
                    </div>

                    <div className="form-group col-md-6">
                      <label className="">Mobile No :</label>
                      <label  className="ml-2"><b>{billInfo.mobile}</b></label>
                   </div>

                   <div className="form-group col-md-6">
                      <label className="">Bill No :</label>
                      <label  className="ml-2"><b>{billInfo.billNo}</b></label>
                   </div>
                   <div className="form-group col-md-6">
                      <label className="">Test Date :</label>
                      <label  className="ml-2"><b>{billInfo.testDate}</b></label>
                   </div>
                   <div className="form-group col-md-6">
                      <label className="">Total Amount :</label>
                      <label  className="ml-2"><b>{billInfo.totalAmount}</b></label>
                   </div>
                   <div className="form-group col-md-6">
                   <div className="float-right">
                      <button type="button" className="btn new_bnt_1 font-weight-bold " onClick={billPayment}> Pay  </button>
                      </div>  
                   </div>

                 </div>
              </div>
            </div>
          </div>
        </div>
      </div>

   
   
    </div>
  );
};

export default PayBill;
