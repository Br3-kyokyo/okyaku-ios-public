class TriggersController < ApplicationController
  before_action :set_trigger_and_transition, only: [:show, :edit, :update, :destroy]

  # GET /trigger
  # GET /trigger.json
  def show
  end

  # GET /trigger/new
  def new
    @trigger = Trigger.new
    @transition = Transition.find(params[:transition_id])
  end

  # GET /trigger/edit
  def edit
  end

  # POST /trigger
  # POST /trigger.json
  def create
    @trigger = Trigger.new(trigger_params)

    respond_to do |format|
      if @trigger.save
        format.html { redirect_to transition_path(@transition), notice: 'Trigger was successfully created.' }
        format.json { render :show, status: :created, location: @trigger }
      else
        format.html { render :new }
        format.json { render json: @trigger.errors, status: :unprocessable_entity }
      end
    end
  end

  # PATCH/PUT /trigger
  # PATCH/PUT /trigger.json
  def update
    respond_to do |format|
      if @trigger.update(trigger_params)
        format.html { redirect_to transition_path(@transition), notice: 'Trigger was successfully updated.' }
        format.json { render :show, status: :ok, location: @trigger }
      else
        format.html { render :edit }
        format.json { render json: @trigger.errors, status: :unprocessable_entity }
      end
    end
  end

  # DELETE /trigger
  # DELETE /trigger.json
  def destroy
    @trigger.destroy
    respond_to do |format|
      format.html { redirect_to transition_path(@transition), notice: 'Trigger was successfully destroyed.' }
      format.json { head :no_content }
    end
  end

  private
    # Use callbacks to share common setup or constraints between actions.
    def set_trigger_and_transition
      @trigger = Trigger.find_by(transition_id: params[:transition_id])
      @transition = @trigger.transition
    end

    # Never trust parameters from the scary internet, only allow the white list through.
    def trigger_params
      params.require(:trigger).permit(:name, :transition_id)
    end
end
