import { Button, DatePicker, Form, Modal, SelectPicker } from "rsuite";
import { BookingByDateForm } from "./BookingByDateForm";
import { BookingByPeriodForm } from "./BookingByPeriodForm";
import { useParams } from "react-router-dom";
import { useState } from "react";

export function BookingTable() {
  const objName = useParams();
  const [bookOption, setbookOption] = useState(true);
  const [open, setOpen] = useState(false);
  const [formValue, setFormValue] = useState({});

  const handleClose = () => {
    setOpen(false);
  };
  const handleOpen = () => {
    setOpen(true);
    setbookOption(false);
  };

  return (
    <div className="dashboard-booking">
      <div>
        Filter
        <div className="f-row">
          <Button onClick={setbookOption(true)}>Book single</Button>
          <Button onClick={handleOpen}>Book many</Button>
        </div>
        {bookOption ? <BookingByDateForm></BookingByDateForm> : null}
        <Modal open={open} onClose={handleClose} size="xs">
          <Modal.Header>
            <Modal.Title>New User</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form fluid onChange={setFormValue} formValue={formValue}>
              <Form.Group controlId="select-dow">
                <Form.ControlLabel>Day of Week </Form.ControlLabel>
                <Form.Control name="dow" data={[]} accepter={SelectPicker} />
              </Form.Group>
              <Form.Group controlId="select-slot">
                <Form.ControlLabel>Slot </Form.ControlLabel>
                <Form.Control name="slot" data={[]} accepter={SelectPicker} />
              </Form.Group>
              <Form.Group controlId="select-datb">
                <Form.ControlLabel>Date begin</Form.ControlLabel>
                <Form.Control name="d-begin" accepter={DatePicker} />
              </Form.Group>
              <Form.Group controlId="select-date">
                <Form.ControlLabel>Date end</Form.ControlLabel>
                <Form.Control name="d-end" accepter={DatePicker} />
              </Form.Group>
            </Form>
          </Modal.Body>
          <Modal.Footer>
            <Button onClick={handleClose} appearance="primary">
              Book
            </Button>
            <Button onClick={handleClose} appearance="subtle">
              Cancel
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
    </div>
  );
}
