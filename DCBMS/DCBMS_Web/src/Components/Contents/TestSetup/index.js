import React,{useEffect, useState} from 'react';
import { NavLink, useHistory } from 'react-router-dom';
import { httpSimpleRequest } from '../../Utils/httpClient';
import { setCookie, getCookie, deleteCookie } from '../../Utils/cookies';
import { formFieldName } from './formField';
import { TextInput } from '../../Form';

let TestSetup = (props) => {
    let [test, setTest] = useState( { testName: "", fee:"",testTypeId:""} );
    let [allTest, setAllTest] = useState( [ {id:1, testName: "1st Test", fee:10, type:1 },  {id:2, testName: "2nd Type", fee:10, type:2} ] );
    let [allTestType, setAllTestType] = useState( [{id:1, testType: "1st Type"}, {id:2, testType: "2st Type"} ] );

    let historyObj = useHistory();
    let routChange = (value) => {
        historyObj.push(value)
    };

    useEffect(
        () => {
            let loginToken = getCookie(process.env.REACT_APP_LOGIN_TOKEN_KEY)
            if (loginToken) {
               // routChange(`/admin/users`);
               console.log("Token Found", loginToken);
               getAllTestType()
            }
        }, []
    );

    const getAllTestType= ()=>{
        console.log("getAllTestType");
        let httpRequest = {
            method: "post",
            url: `${process.env.REACT_APP_API_HOST_URL}/test-type`,
            data: {},
            headers: {
                'content-type':'application/json'
            }
        };

        httpSimpleRequest(httpRequest)
            .then(response => {
                console.log("response", response.data);
                if (response?.data) {
                    setAllTestType(response.data);
                }

            }).catch(error => {
                console.log("error", error);
                let notifyOptions={
                    title: "Error",
                    message: "Incorrect username or password.",
                    type: "danger"
                }; 
            })
    }

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
            if (name !== "") { signinObj[name] = value; }
        };

        console.log("signinObj", signinObj)

        let httpRequest = {
            method: "post",
            url: `${process.env.REACT_APP_API_HOST_URL}/test-setup`,
            data: signinObj,
            headers: {
                'content-type':'application/json'
            }
        }

        httpSimpleRequest(httpRequest)
            .then(response => {
                console.log("response", response.data);
                if (response?.data) {
                    setAllTest(response.data);
                }
            }).catch(error => {
                console.log("error", error);
                let notifyOptions={
                    title: "Error",
                    message: "Incorrect username or password.",
                    type: "danger"
                }; 
            })
    }

   const testTypeFilter=(id)=>{
       let testType=  ""
     allTestType.filter((typeItem, newIndex)=>{
         if (typeItem.id == id) {
             testType = typeItem.testType
         }
        })
        return testType;
    }

    return (
        <div className="row justify-content-center mt-5">
            <div className="col-12 col-md-12 col-xl-10 col-lg-10 col-sm-12">
                <div className="container custom_form mt-5">
                    <div className="row mt-0 mr-n4 ml-n4">
                        <div className="col-12">
                            <div className="container">
                                <div className="row justify-content-center  font-weight-bold h3 "><div className="col-12 mx-0  border-top-0 border-right-0 border-left-0  border-bottom text-center pb-2">Test Setup</div></div>
                                <form onSubmit={handleSubmit}>
                                    <div className="row justify-content-center  mx-2">
                                        {
                                            formFieldName.map((item, itemIndex) => {
                                                return <TextInput
                                                    key={itemIndex}
                                                    {...item}
                                                    value={test.testType}
                                                    error=""
                                                    onChange={handleChange}
                                                />
                                            })
                                        }
                                    </div>
                                    <div className="row  mx-2">
                                        <div className="form-group col-md-6 ">

                                            <select
                                               // value={test.testTypeId}
                                                defaultValue={test.testTypeId}
                                                className="form-control mb-2 "
                                                name="testTypeId"
                                                onChange={handleChange} >
                                                {allTestType.map((item, index) => {
                                                    return (
                                                        <option
                                                            key={index}
                                                            value={item.id}
                                                        >{item.testType}
                                                        </option>
                                                    )
                                                })}
                                            </select>
                                        </div>
                                    </div>
                                    <div className="row mx-2 justify-content-center">
                                        <div className="col-2 ">
                                            <button type="submit" className="btn new_bnt_1 font-weight-bold ">Save</button>
                                        </div>
                                    </div>
                                </form>
                                <div className="row justify-content-center border mt-5" >
                                <table className="table mx-2 my-2 table-bordered" >
                                    <thead className="thead-light">
                                        <tr>
                                            <th scope="col">SL</th>
                                            <th scope="col">Test Name</th>
                                            <th scope="col">Fee</th>
                                            <th scope="col">Type</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {
                                            allTest.map((item,index)=>{
                                                return <tr key={index}>
                                                <td scope="row">{item.id}</td>
                                                <td scope="row">{item.testName}</td>
                                                <td scope="row">{item.fee}</td>
                                                <td scope="row">{ testTypeFilter(item.type) }</td>
                                            </tr>
                                            })
                                        }
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
}

export default TestSetup;
