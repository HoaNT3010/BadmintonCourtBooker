import SearchBarTextOnly from "../../../common/SearchBarTextOnly";
import { CourtResultTable } from "./CourtResultTable";
import { FilterBox } from "./FilterBox";

export function Dashboard(){
    return(
        <div className="dashboard-court f-row">
            <FilterBox>

            </FilterBox>
            <div className="f-col">
            <SearchBarTextOnly 
            filterText = {filterText}
            onFilterTextChange = {setFilterText}/>
            <CourtResultTable>

            </CourtResultTable>
            </div>
        </div>
    )
}
