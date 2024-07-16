import { Card } from "react-bootstrap";
import { Button, Container } from "rsuite";

export function BookingItemDetail({ booking }) {
  return (
    <div>
      <Card className="item-card">
        <Card.Body>
          <Card.Title>{booking.name}</Card.Title>
        </Card.Body>
        <Button variant="success">Check in</Button>
      </Card>
    </div>
  );
}
