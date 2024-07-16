import { Button, DatePicker, Table } from "rsuite";
import { SlotItem } from "./SlotItem";

export function BookingByDateForm({id}){
    const [dateStart, setdateStart] = useState();
    const [dateEnd, setdateEnd] = useState();
    const[api, setAPI] = useState([]);

    const handleFind = () => {
        fetch(url)
        .then(response => response.json())
        .then(data => setAPI(data))
        .catch(error => console.log(error.message));
    };

    useEffect(() => {
        fetch(url)
        .then(response => response.json())
        .then(data => setAPI(data))
        .catch(error => console.log(error.message));
    }, [])

    return(
        <div className="table-schedule-date f-col">
            <div>
                filter
                <DatePicker format="dd/MM/yyyy" onChange={setdateStart(e.target.value)}></DatePicker>
                <DatePicker format="dd/MM/yyyy" onChange={setdateEnd(e.target.value)}></DatePicker>
                <Button onClick={handleFind}>Find</Button>
            </div>
            <div>
            <Table striped className="table-admin">
        <thead>
          <tr>
            <th>Date</th>
            <th>Slot Available</th>
          </tr>
        </thead>
        <tbody>
          {api.map((item) => (
            <tr>
              <td>{item.id}</td>
              <td>
                <SlotItem objectList={item.slots}></SlotItem>
              </td>
              {/* <td>
                <div className="admin-table-button d-flex flex-row">
                  <Link to={`/admin/udpate/${item.id}`}>
                    <Button variant="success">Edit</Button>
                  </Link>
                  &nbsp;&nbsp;
                  <Button variant="danger" onClick={() => handleDelete(item.id)}>Delete</Button>
                </div>
              </td> */}
            </tr>
          ))}
        </tbody>
      </Table>
            </div>
        </div>
    )
}
