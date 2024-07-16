import { Form } from "react-bootstrap";

export default function SearchBarTextOnly({filterText, onFilterTextChange}) {
    return (
      <Form.Control 
        type="text" value={filterText} placeholder="Flower's Name" onChange={(e) => onFilterTextChange(e.target.value)} >
      </Form.Control>
    );
  }
  