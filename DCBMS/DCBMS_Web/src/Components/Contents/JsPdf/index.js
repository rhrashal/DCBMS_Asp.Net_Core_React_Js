  import React from "react";
  import jsPDF from "jspdf";
  import "jspdf-autotable";
  
  let JsPDFPrint = (props) => {
    //console.log("1",props);
    const generatePDF = () => {
        //console.log("2",props);
        const doc = new jsPDF();
    

       // console.log([["Name", "Email", "Country", "last"]])
       // console.log(props.header);
        // It can parse html:
        // <table id="my-table"><!-- ... --></table>
        doc.autoTable({ html: "#my-table" });
    
        // Or use javascript directly:
        doc.autoTable({
          head: props.header,// props.header , //[["Name", "Email", "Country"]],
          body:  props.body,
        //   body: [
        //     ["David", "david@example.com", "Sweden"],
        //     ["Castille", "castille@example.com", "Spain"],
        //     // ...
        //   ],
        });
    
        doc.save("table.pdf");
      };
  
    return (
      <div>
        <button className="btn btn-warning" onClick={generatePDF} type="primary">
           PDF
        </button>
      </div>
    );
  };
  
  export default JsPDFPrint;
  