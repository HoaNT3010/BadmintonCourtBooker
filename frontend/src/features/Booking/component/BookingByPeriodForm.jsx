import { Button, Card } from "react-bootstrap";

export function BookingByPeriodForm(id){
    return(
        <div className="f-row">
            <div>
            
            </div>
            <Card className="item-card">
        <Card.Img variant="top" src={object.picture} ></Card.Img>
        <Card.Body>
            <Card.Title>{object.name}</Card.Title>
        </Card.Body>
        <Button variant="success">Detail</Button>
      </Card>
        </div>
    )
}
