class StateMachinesController < ApplicationController
  before_action :set_state_machine, only: [:show, :edit, :update, :destroy]

  # GET /state_machines
  # GET /state_machines.json
  def index
    @state_machines = StateMachine.where(scenario_category_id: params[:scenario_category_id])
  end

  # GET /state_machines/1
  # GET /state_machines/1.json
  def show
    @states = @state_machine.states
    @transitions = @state_machine.transitions
  end

  # GET /state_machines/new
  def new
    @state_machine = StateMachine.new
  end

  # GET /state_machines/1/edit
  def edit
  end

  # POST /state_machines
  # POST /state_machines.json
  def create
    @state_machine = StateMachine.new(state_machine_params)

    respond_to do |format|
      if @state_machine.save
        format.html { redirect_to @state_machine, notice: 'State machine was successfully created.' }
        format.json { render :show, status: :created, location: @state_machine }
      else
        format.html { render :new }
        format.json { render json: @state_machine.errors, status: :unprocessable_entity }
      end
    end
  end

  # PATCH/PUT /state_machines/1
  # PATCH/PUT /state_machines/1.json
  def update
    respond_to do |format|
      if @state_machine.update(state_machine_params)
        format.html { redirect_to @state_machine, notice: 'State machine was successfully updated.' }
        format.json { render :show, status: :ok, location: @state_machine }
      else
        format.html { render :edit }
        format.json { render json: @state_machine.errors, status: :unprocessable_entity }
      end
    end
  end

  # DELETE /state_machines/1
  # DELETE /state_machines/1.json
  def destroy
    @state_machine.destroy
    respond_to do |format|
      format.html { redirect_to state_machines_url, notice: 'State machine was successfully destroyed.' }
      format.json { head :no_content }
    end
  end

  private
    # Use callbacks to share common setup or constraints between actions.
    def set_state_machine
      @state_machine = StateMachine.find(params[:id])
    end

    # Never trust parameters from the scary internet, only allow the white list through.
    def state_machine_params
      params.require(:state_machine).permit(:name, :scenario_category_id, :position)
    end
end
